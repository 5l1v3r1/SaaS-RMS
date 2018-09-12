using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Encryption;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.RestaurantControllers
{
    public class AppUsersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _hostingEnv;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public AppUsersController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnv = hostingEnvironment;
        }

        #endregion

        #region Index

        //GET: AppUser/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            var appUser = _db.AppUsers.Where(au => au.RestaurantId == restaurant)
                .Include(au => au.Role);
            if (appUser != null)
            {
                return View(await appUser.ToListAsync());
            }
            else
            {
                return RedirectToAction("Access", "Restaurant");
            }
        }

        #endregion

        #region Create

        //GET: AppUsers/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.RoleId = new SelectList(_db.Roles, "RoleId", "Name");
            ViewBag.EmployeeId = new SelectList(_db.EmployeePersonalDatas, "EmployeeId", "DisplayName");
            return View();
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser appUser)
        {
            try
            {
                //var userid = _session.GetInt32("loggedinuserid");
                var restaurantid = _session.GetInt32("restaurantsessionid");
                var role = _db.Roles.Find(appUser.Role);
                appUser.RestaurantId = Convert.ToInt32(restaurantid);
                //appUser.CreatedBy = userid;
                //appUser.LastModifiedBy = userid;
                appUser.DateCreated = DateTime.Now;
                appUser.DateLastModified = DateTime.Now;

                //Generate Password
                var generator = new Random();
                var number = generator.Next(0, 1000000).ToString("D6");

                appUser.Password = new Hashing().HashPassword(number);
                appUser.ConfirmPassword = appUser.Password;

                if(_db.AppUsers.Where(au => au.Email == appUser.Email).ToList().Count > 0)
                {
                    TempData["appuser"] = "A user with the same email already exist!";
                    TempData["notificationtype"] = NotificationType.Error.ToString();
                    return View(appUser);
                }

                
                _db.AppUsers.Add(appUser);
                await _db.SaveChangesAsync();

                if (appUser.AppUserId > 0)
                {
                    //define acceskeys and save transactions
                    var accessKey = new AppUserAccessKey
                    {
                        PasswordAccessCode = new Md5Encryption().RandomString(15),
                        AccountActivationAccessCode = new Md5Encryption().RandomString(20),
                        CreatedBy = appUser.AppUserId,
                        LastModifiedBy = appUser.AppUserId,
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now,
                        ExpiryDate = DateTime.Now.AddDays(1),
                        AppUserId = appUser.AppUserId
                    };

                    _db.AppUserAccessKeys.Add(accessKey);
                    await _db.SaveChangesAsync();
                    //new Mailer().SendNewUserEmail("", appUser, role, accessKey);
                }
                TempData["appuser"] = "You have successfully added a new user!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                //display notification
                TempData["appuser"] = ex.Message;
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return View(appUser);
            }
        }

        #endregion

        #region Edit

        //GET: AppUsers/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var appUser = _db.AppUsers.Find(id);
            ViewBag.RoleId = new SelectList(_db.Roles, "RoleId", "Name", appUser.RoleId);
            return View(appUser);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(AppUser appUser, IFormFile Logo)
        {
            try
            {
                var userid = _session.GetInt32("loggedinuser");
                appUser.LastModifiedBy = Convert.ToInt32(userid);
                appUser.DateLastModified = DateTime.Now;

                if(_db.AppUsers.Where(au => au.Email == appUser.Email && au.AppUserId != appUser.AppUserId).ToList().Count > 0)
                {
                    TempData["appuser"] = "A user with the same email already exist!";
                    TempData["notificationtype"] = NotificationType.Error.ToString();
                    return View(appUser);
                }

                //Upload user Logo if any file is uploaded
                if (Logo != null && !string.IsNullOrEmpty(Logo.FileName))
                {
                    var fileInfo = new FileInfo(Logo.FileName);
                    var ext = fileInfo.Extension.ToLower();
                    var name = DateTime.Now.ToFileTime().ToString();
                    var fileName = name + ext;
                    var uploads = _hostingEnv.WebRootPath + $@"\UpLoads\ProfilePicture\{fileName}";

                    using (var fileStream = System.IO.File.Create(uploads))
                    {
                        if (fileStream != null)
                        {
                            Logo.CopyTo(fileStream);
                            fileStream.Flush();
                        }
                    }
                }

                _db.Entry(appUser).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                TempData["appuser"] = "You have successfully modified the user!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //display notification
                TempData["appuser"] = ex.Message;
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return View(appUser);
            }
        }

        #endregion
        
        #region Delete

        // GET: AppUsers/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _db.AppUsers
                .Include(a => a.Restaurant)
                .Include(a => a.Role)
                .SingleOrDefaultAsync(m => m.AppUserId == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return PartialView("Delete", appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var appUser = await _db.AppUsers.SingleOrDefaultAsync(m => m.AppUserId == id);
                _db.AppUsers.Remove(appUser);
                await _db.SaveChangesAsync();

                TempData["appuser"] = "You have successfully deleted the user!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //display notification
                TempData["appuser"] = ex.Message;
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return RedirectToAction("Index");
            }
        }
        
        #endregion

        #region AppUserExists

        private bool AppUserExists(int id)
        {
            return _db.AppUsers.Any(e => e.AppUserId == id);
        }

        #endregion

    }
}

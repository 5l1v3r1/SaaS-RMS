using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Encryption;
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.SystemControllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public AccountController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Account Activation

        public async Task<IActionResult> AccountActivation(string accessCode)
        {
            var accessKey = _db.AppUserAccessKeys.SingleOrDefault(ak => ak.AccountActivationAccessCode == accessCode);
            var appUser = _db.AppUsers.SingleOrDefault(au => accessKey != null && au.AppUserId == accessKey.AppUserId);

            if(appUser != null)
            {
                if(appUser.Status == UserStatus.Inactive.ToString())
                {
                    //update user
                    appUser.Status = UserStatus.Active.ToString();
                    _db.Entry(appUser).State = EntityState.Modified;
                    await _db.SaveChangesAsync();

                    if(accessKey != null)
                    {
                        //update accesskeys
                        accessKey.AccountActivationAccessCode = new Md5Encryption().RandomString(24);
                        accessKey.DateLastModified = DateTime.Now;
                        accessKey.ExpiryDate = DateTime.Now.AddDays(1);

                        _db.Entry(accessKey).State = EntityState.Modified;
                        await _db.SaveChangesAsync();

                        //Display Notification
                        TempData["account"] ="You have successfully verified your account, Login and Enjoy the Experience!";
                        TempData["notificationtype"] = NotificationType.Success.ToString();
                        return RedirectToAction("Login", "Account");
                    }
                }
                
                if(appUser.Status == UserStatus.Active.ToString())
                {
                    //Display Notification
                    TempData["account"] ="You have already activated your account, use your username and password to login!";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Login", "Account");
                }
            }

            //Display notification
            TempData["account"] ="Your Reuqest is Invalid, Try again Later!";
            TempData["notificationtype"] = NotificationType.Error.ToString();
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region Login

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if(_session.GetInt32("loggedinuserid") == null)
            {
                return View();
            }

            if(_session.GetInt32("loggedinuserid") != null)
            {
                return RedirectToAction("Dashboard", "Home");
            }

            if(returnUrl != null && returnUrl == "logOut")
            {
                _db.Dispose();
                _session.Clear();

                //display notification
                TempData["display"] = "You have succesfully Logged Out of Foodee!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
            }

            if(returnUrl != null && returnUrl == "sessionExpired")
            {
                _db.Dispose();
                _session.Clear();

                //display notification
                TempData["display"] = "Your session has expired, Login to continue!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountModel model, IFormCollection collection)
        {
            var access = new AccessLog();
            var user = await _db.AppUsers
                .Include(au => au.Restaurant)
                .Include(au => au.Role)
                .SingleOrDefaultAsync(au => au.Email.ToLower() == model.Email.ToLower());
            try
            {
                if(user == null)
                {
                    access.Message = "The Account does not exist, Try again!";
                    access.Status = AccessStatus.Denied.ToString();
                    access.Category = AccessCategory.Login.ToString();

                }
            }

        }

        #endregion





        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
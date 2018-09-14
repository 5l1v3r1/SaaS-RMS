using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Encryption;
using SaaS_RMS.Models.Entities.Employee;
using SaaS_RMS.Models.Entities.Restuarant;
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
                if (user == null)
                {
                    access.Message = "The Account does not exist, Try again!";
                    access.Status = AccessStatus.Denied.ToString();
                    access.Category = AccessCategory.Login.ToString();
                    access.DateCreated = DateTime.Now;
                    access.DateLastModified = DateTime.Now;

                    await _db.AccessLogs.AddAsync(access);
                    await _db.SaveChangesAsync();
                }
                else
                {
                    if(user.Status == UserStatus.Inactive.ToString())
                    {
                        access.Message ="You are yet to activate your account from the the link sent to your email when you created the account!";
                        access.Status = AccessStatus.Denied.ToString();
                        access.Category = AccessCategory.Login.ToString();
                        access.DateCreated = DateTime.Now;
                        access.DateLastModified = DateTime.Now;
                        access.CreatedBy = user.AppUserId;
                        access.LastModifiedBy = user.AppUserId;

                        await _db.AccessLogs.AddAsync(access);
                        await _db.SaveChangesAsync();
                        user = null;
                    }
                    var passwordCorrect = user != null && new Hashing().ValidatePassword(model.Password, user.ConfirmPassword);
                    if(passwordCorrect == false)
                    {
                        if(user != null)
                        {
                            access.Message = "Your login information is Incorrect, Check and Try again!";
                            access.Status = AccessStatus.Denied.ToString();
                            access.Category = AccessCategory.Login.ToString();
                            access.DateCreated = DateTime.Now;
                            access.DateLastModified = DateTime.Now;
                            access.CreatedBy = user.AppUserId;
                            access.LastModifiedBy = user.AppUserId;

                            await _db.AccessLogs.AddAsync(access);
                            await _db.SaveChangesAsync();
                            user = null;
                        }
                    }
                    if(passwordCorrect == true)
                    {
                        access.Message = "Dear " + user.Name + ", You have successfully logged in!";
                        access.Status = AccessStatus.Approved.ToString();
                        access.Category = AccessCategory.Login.ToString();
                        access.DateCreated = DateTime.Now;
                        access.AppUserId = user.AppUserId;
                        access.DateLastModified = DateTime.Now;
                        access.CreatedBy = user.AppUserId;
                        access.LastModifiedBy = user.AppUserId;

                        await _db.AccessLogs.AddAsync(access);
                        await _db.SaveChangesAsync();

                        //store user sessions
                        _session.SetInt32("loggedinuserid", user.AppUserId);
                        _session.SetInt32("loggedinemployeeid", user.EmployeeId);
                        _session.SetString("loggedinuser", JsonConvert.SerializeObject(user));

                        //display notification
                        TempData["account"] = access.Message;
                        TempData["notificationtype"] = NotificationType.Success.ToString();
                        return RedirectToAction("Dashboard", "Home");
                    }
                }

                //display notification
                TempData["account"] = access.Message;
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return View(model);
            }
            catch (Exception)
            {
                //display notification
                TempData["account"] = "Unexpected Error Occured while trying to login!";
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return View(model);
            }
        }


        #endregion

        #region LogOut

        [HttpGet]
        public IActionResult LogOut()
        {
            _db.Dispose();
            _session.Clear();
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion

        #region First Registeration

        [HttpGet]
        public IActionResult FirstRegistration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FirstRegistration(AppUser appUser)
        {
            var restaurantid = _session.GetInt32("restaurantsessionid");
            
            if (restaurantid != null)
            {
                var _firstEmployee = new Employee()
                {
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    RestaurantId = Convert.ToInt32(restaurantid)
                };
                await _db.Employees.AddAsync(_firstEmployee);
                await _db.SaveChangesAsync();

                var _firstRole = new Role()
                {
                    Name = "SuperUser",
                    CanDoSomething = true,
                    CanManageEmployee = true,
                    CanManageOrders = true,
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    RestaurantId = Convert.ToInt32(restaurantid)

                };
                await _db.Roles.AddAsync(_firstRole);
                await _db.SaveChangesAsync();

                var _firstAppUser = new AppUser()
                {
                    Name = appUser.Name,
                    Email = appUser.Email,
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    Password = BCrypt.Net.BCrypt.HashPassword(appUser.Password),
                    ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(appUser.ConfirmPassword),
                    EmployeeId = _firstEmployee.EmployeeId,
                    RestaurantId = Convert.ToInt32(restaurantid),
                    RoleId = _firstRole.RoleId
                };
                await _db.AppUsers.AddAsync(_firstAppUser);
                await _db.SaveChangesAsync();
            }
            else
            {
                return RedirectToAction("Access", "Restaurant");
            }

            var _restaurant = _db.Restaurants.Find(restaurantid);
            ViewData["restaurantname"] = _restaurant.Name;
            return RedirectToAction("Signin", "Account");
        }

        #endregion

        #region Signin

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }

        #endregion






    }
}
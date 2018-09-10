using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers
{
    public class RMS_AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public RMS_AdminController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Dashbaord

        //GET: RMS_Admin/Login
        [HttpGet]
        public async Task<IActionResult> Welcome()
        {
            var rmsuser = await _db.RMSUsers.CountAsync();
            if (rmsuser > 1)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("FirstRegistration");
            }

        }

        #endregion

        #region Login

        //GET: RMS_Admin/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        #endregion


        #region Creating Super User

        //GET: Account/FirstRegister
        [HttpGet]
        [AllowAnonymous]
        public IActionResult FirstRegistration()
        {
            return View();
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FirstRegistration(RMSUser user)
        {
            if (user != null)
            {
                var _role = new RMSRole
                {
                    Name = "SuperUser",
                    CanManageRestaurants = true
                };

                await _db.RMSRoles.AddAsync(_role);
                await _db.SaveChangesAsync();

                var _user = new RMSUser()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    ConfirmPassword = user.ConfirmPassword,
                    RMSRoleId = _role.RMSRoleId
                };

                await _db.RMSUsers.AddAsync(_user);
                await _db.SaveChangesAsync();

                TempData["firstregistration"] = "You have successfully Created a RMS SUPER USER!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return RedirectToAction("Login", "RMS_Admin");
            }
            return View(user);
        }

        #endregion




    }
}
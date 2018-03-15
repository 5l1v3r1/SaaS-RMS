using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Data_Factory;
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace SaaS_RMS.Controllers.SystemControllers
{
    public class RestaurantsController : Controller
    {
        private readonly ApplicationDbContext _db;

        #region Constructor

        public RestaurantsController(ApplicationDbContext context)
        {
            _db = context;
        }

        #endregion

        #region Fetch Data

        /// <summary>
        /// Sends Json responds object to view with lga of the state requested via an Ajax call
        /// </summary>
        /// <param name="id"> state id</param>
        /// <returns>lgas</returns>
        /// Microsoft.CodeDom.Providers.DotNetCompilerPlatform
        //public JsonResult GetLgaForState(int id)
        //{
        //    var lgas = new StateFactory().GetLgaForState(id);
        //    return Json(lgas, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        #region Restaurant Index

        public async Task<IActionResult> Index()
        {
            return View(await _db.Restaurants.ToListAsync());
        }

        #endregion

        #region Restaurant Register

        // GET: /Restaurant/Register
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.StateId = new SelectList(_db.States, "StateId", "Name");
            return View();
        }

        // POST: /Restaurant/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                if (await _db.Restaurants.AnyAsync(r => r.Name == restaurant.Name))
                {
                    ModelState.AddModelError("", "Restaurant name already exists");
                }

                if(await _db.Restaurants.AnyAsync(r => r.ContactEmail == restaurant.ContactEmail))
                {
                    ModelState.AddModelError("", "Contact Email already exists");
                }

                else
                {
                    _db.Restaurants.Add(restaurant);
                    _db.SaveChanges();

                    TempData["Success"] = restaurant.Name + " have successfully joined the Odarms community";
                    TempData["notificationType"] = NotificationType.Success.ToString();
                    return Json(new { success = true });
                }
                
            }
            ViewBag.StateId = new SelectList(_db.States, "StateId", "Name", restaurant.StateId);
            return View();
        }

        #endregion

        #region Restaurant Access

        //GET: Restaurant/Access
        [HttpGet]
        public IActionResult Access()
        {
            return View();
        }

        //POST: 
        [HttpPost]
        public async Task<IActionResult> Access(Restaurant restaurant)
        {
            //int restaurantId;

            //var rest = _db.Restaurants.SingleAsync(r => r.Name == restaurant.Name && r.AccessCode == restaurant.AccessCode);

            if (await _db.Restaurants.AnyAsync(r => r.Name == restaurant.Name && r.AccessCode == restaurant.AccessCode))
            {
                //HttpContext.Session.SetInt32("RId", restaurantId);
                return RedirectToAction("Login", "Employee");
            }
            else
            {
                ModelState.AddModelError("", "Restaurant Name doesn't match the access code");
            }

            return View();
        }
        #endregion

    }
}
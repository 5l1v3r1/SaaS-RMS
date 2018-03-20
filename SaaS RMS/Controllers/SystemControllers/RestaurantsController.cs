using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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

        public JsonResult GetLgasForState(int id)
        {
            var lgas = _db.Lgas.Where(l => l.StateId == id);
            return Json(lgas);
        }

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
            var rest = await _db.Restaurants.FirstOrDefaultAsync(r => r.Name == restaurant.Name && r.AccessCode == restaurant.AccessCode);
            if (rest != null)
            {
                return RedirectToAction("Admin", new { ID = rest.RestaurantId });
            }
            else
            {
                ViewData["mismatch"] = "Restaurant name doesn't match the access code";
            }

            return View();
        }
        #endregion

        #region Restaurant Admin

        
        public IActionResult Admin(int? ID)
        {
            int _ID = Convert.ToInt32(ID);
            HttpContext.Session.SetInt32("RId", _ID);
            return View();
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.RestaurantController
{
    public class RestaurantQualificationsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public RestaurantQualificationsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Restaurant Qualification Index

        public async Task<IActionResult> Index()
        {
            var restaurant = _session.GetInt32("");

            if (restaurant == null)
            {
                return RedirectToAction("Access", "Restaurants");
            }

            var restaurantQualification = _db.RestaurantQualifications.Where(d => d.RestaurantId == restaurant)
                             .Include(d => d.Restaurant);

            if (restaurantQualification != null)
            {
                return View(await restaurantQualification.ToListAsync());
            }
            else
            {
                return RedirectToAction("Access", "Restaurants");
            }
        }

        #endregion

        #region Restaurant Qualification Create

        //GET: RestaurantQualifications/Create
        [HttpGet]
        public IActionResult Create()
        {
            var restaurantQualification = new RestaurantQualification();
            return PartialView("Create", restaurantQualification);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RestaurantQualification restaurantQualification)
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            if (ModelState.IsValid)
            {
                if (restaurant != null)
                {
                    restaurantQualification.RestaurantId = Convert.ToInt32(restaurant);

                    var allRestaurantQualifications = await _db.RestaurantQualifications.ToListAsync();
                    if (allRestaurantQualifications.Any(d => d.Name == restaurantQualification.Name && d.RestaurantId == restaurant))
                    {
                        TempData["restaurantQualification"] = "You cannot add " + restaurantQualification.Name + " Qualification because it already exist!!!";
                        TempData["notificationType"] = NotificationType.Error.ToString();
                        return RedirectToAction("Index");
                    }

                    await _db.AddAsync(restaurantQualification);
                    await _db.SaveChangesAsync();

                    TempData["restaurantQualification"] = "You have successfully added " + restaurantQualification.Name + " new Qualification!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();
                    return Json(new { success = true });
                }

                else
                {
                    TempData["restaurantQualification"] = "Session Expired,Login Again";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Restaurant", "Access");
                }
            }

            return RedirectToAction("Index");

        }

        #endregion

        #region Restaurant Qualification Edit

        //GET: RestaurantQualifications/Edit/4
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantQualification = await _db.RestaurantQualifications.SingleOrDefaultAsync(d => d.RestaurantQualificationId == id);

            if (restaurantQualification == null)
            {
                return NotFound();
            }

            return PartialView("Edit", restaurantQualification);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, RestaurantQualification restaurantQualification)
        {
            if (id != restaurantQualification.RestaurantQualificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var restaurant = _session.GetInt32("restaurantsessionid");
                    if (restaurant != null)
                    {
                        restaurantQualification.RestaurantId = Convert.ToInt32(restaurant);
                    }


                    _db.Update(restaurantQualification);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantQualificationExists(restaurantQualification.RestaurantQualificationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["restaurantQualification"] = "You have successfully modified " + restaurantQualification.Name + " qualification!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Restaurant Qualification Delete

        //GET: RestaurantQualifications/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RestaurantQualification restaurantQualification = await _db.RestaurantQualifications
                .Include(rq => rq.Restaurant)
                .SingleOrDefaultAsync(d => d.RestaurantQualificationId == id);

            if (restaurantQualification == null)
            {
                return NotFound();
            }

            return PartialView("Delete", restaurantQualification);
        }

        //POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            RestaurantQualification restaurantQualification = await _db.RestaurantQualifications.SingleOrDefaultAsync(d => d.RestaurantQualificationId == id);

            if (restaurantQualification != null)
            {
                _db.RestaurantQualifications.Remove(restaurantQualification);
                await _db.SaveChangesAsync();

                TempData["restaurantQualification"] = "You have successfully deleted " + restaurantQualification.Name + " Qualification!";
                TempData["notificationtype"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Restaurant Qualification Exists

        private bool RestaurantQualificationExists(int id)
        {
            return _db.RestaurantQualifications.Any(e => e.RestaurantQualificationId == id);
        }

        #endregion

    }
}
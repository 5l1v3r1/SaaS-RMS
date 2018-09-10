using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.SystemControllers
{
    public class RestaurantSubscriptionsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public RestaurantSubscriptionsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index(int PackageId)
        {
            try
            {
                var restaurantsubscriptions = await _db.RestaurantSubscriptions.Where(rs => rs.PackageId == PackageId).ToListAsync();

                if (restaurantsubscriptions == null)
                {
                    return NotFound();
                }

                return View(restaurantsubscriptions);
            }
            catch(Exception e)
            {
                return Json(e);
            }
        }

        #endregion

        #region Create

        //GET: RestaurantSubscriptions/Create
        [HttpGet]
        public IActionResult Create()
        {
            var restaurantSubcription = new RestaurantSubscription();
            return PartialView("Create", restaurantSubcription);
        }
        
        //POST:
        [HttpPost]
        public async Task<IActionResult> Create(RestaurantSubscription restaurantSubscription)
        {
            if (ModelState.IsValid)
            {
                var allRestaurantSubscriptions = await _db.RestaurantSubscriptions.ToListAsync();
                if(allRestaurantSubscriptions.Any(rs => rs.Duration == restaurantSubscription.Duration && 
                                                        rs.Package.Name == restaurantSubscription.Package.Name))
                {
                    TempData["restaurantsubscription"] = "You cannot add that subscription because it already exist!!!";
                    TempData["notificationType"] = NotificationType.Error.ToString();
                    return RedirectToAction("Index");
                }

                var _restaurantSubcription = new RestaurantSubscription
                {
                    DateCreated = DateTime.Now,
                    CreatedBy = _session.GetInt32("loggedinuser"),
                    DateLastModified = DateTime.Now,
                    LastModifiedBy = Convert.ToInt32(_session.GetInt32("loggedinuser"))
                };

                await _db.RestaurantSubscriptions.AddAsync(restaurantSubscription);
                await _db.SaveChangesAsync();

                return Json(new { success = true });
            }
            return View("Index", new { PackageId = restaurantSubscription.PackageId });
        }

        #endregion

        #region Edit

        //GET: RestauarantSubscriptions/Edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantSubscription = await _db.RestaurantSubscriptions.SingleOrDefaultAsync(rs => rs.RestaurantSubscriptionId == id);

            if (restaurantSubscription == null)
            {
                return NotFound();
            }

            return PartialView("Edit", restaurantSubscription);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, RestaurantSubscription restaurantSubscription)
        {
            if (id != restaurantSubscription.RestaurantSubscriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(restaurantSubscription);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantSubscriptionExist(restaurantSubscription.RestaurantSubscriptionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["restaurantsubscription"] = "You have successfully modified a subscription !!!";
                TempData["notificationType"] = NotificationType.Success.ToString();
                return Json(new { success = true });
            }

            return RedirectToAction("Index", new { PackageId = restaurantSubscription.PackageId });
        }

        #endregion

        #region Delete

        //GET: RestaurantSubscriptions/Delete/id
        [HttpGet]
        public async Task <IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantSubscription = await _db.RestaurantSubscriptions
                .Include(rs => rs.Package)
                .SingleOrDefaultAsync(rs => rs.RestaurantSubscriptionId == id);

            if (restaurantSubscription == null)
            {
                return NotFound();
            }

            return PartialView("Delete", restaurantSubscription);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var restaurantSubscription = await _db.RestaurantSubscriptions.SingleOrDefaultAsync(rs => rs.RestaurantSubscriptionId == id);
            if(restaurantSubscription == null)
            {
                _db.RestaurantSubscriptions.Remove(restaurantSubscription);
                await _db.SaveChangesAsync();

                TempData["restaurantsubscription"] = "You have successfully deleted a subscription!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();
                return Json(new { success = true });
            }
            return RedirectToAction("Index", new { PackageId = restaurantSubscription.PackageId });
        }

        #endregion

        #region RestaurantSubscriptionExist

        private bool RestaurantSubscriptionExist(int id)
        {
            return _db.RestaurantSubscriptions.Any(rs => rs.RestaurantSubscriptionId == id);
        }

        #endregion

    }
}
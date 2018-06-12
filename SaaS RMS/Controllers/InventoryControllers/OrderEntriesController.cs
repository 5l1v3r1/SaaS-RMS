using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Inventory;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.InventoryControllers
{
    public class OrderEntriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public OrderEntriesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Index
        
        public async Task<IActionResult> Index()
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            if (restaurant == null)
            {
                return RedirectToAction("Access", "Restaurants");
            }
            else
            {
                var orderEntry = await _db.OrderEntries.Where(p => p.RestaurantId == restaurant)
                    .ToListAsync();

                return View(orderEntry);
            }
        }

        #endregion

        #region Create

        //GET:
        [HttpGet]
        public IActionResult Create()
        {
            var orderEntry = new OrderEntry();
            return PartialView("Create", orderEntry);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderEntry orderEntry)
        {
            var restaurant = _session.GetInt32("restaurantsessionid");
            if (ModelState.IsValid)
            {
                if (restaurant != null)
                {
                    orderEntry.RestaurantId = Convert.ToInt32(restaurant);

                    await _db.AddAsync(orderEntry);
                    await _db.SaveChangesAsync();

                    TempData["orderentry"] = "You have successfully added " + orderEntry.Name + " as a new Order Entry!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();

                    return Json(new { success = true });
                }
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit

        //GET: OrderEntries/Edit/6
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderEntry = await _db.OrderEntries.SingleOrDefaultAsync(p => p.OrderEntryId == id);

            if (orderEntry == null)
            {
                return NotFound();
            }

            return PartialView("Edit", orderEntry);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, OrderEntry orderEntry)
        {
            if (id != orderEntry.OrderEntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var restaurant = _session.GetInt32("restaurantsessionid");

                if (restaurant != null)
                {
                    orderEntry.RestaurantId = Convert.ToInt32(restaurant);

                    _db.Update(orderEntry);
                    await _db.SaveChangesAsync();

                    TempData["orderentry"] = "You have successfully modified " + orderEntry.Name + " Order Entry!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();
                    return Json(new { success = true });
                }
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        //GET: OrderEntries/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderEntry = await _db.OrderEntries.SingleOrDefaultAsync(b => b.OrderEntryId == id);

            if (orderEntry == null)
            {
                return NotFound();
            }

            return PartialView("Delete", orderEntry);
        }

        //POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderEntry = await _db.OrderEntries.SingleOrDefaultAsync(b => b.OrderEntryId == id);
            if (orderEntry != null)
            {
                _db.OrderEntries.Remove(orderEntry);
                await _db.SaveChangesAsync();

                TempData["orderentry"] = "You have successfully deleted " + orderEntry.Name + " Order Entry!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Order Entries Exists

        private bool OrderEntriesExists(int id)
        {
            return _db.OrderEntries.Any(p => p.OrderEntryId == id);
        }

        #endregion

    }
}
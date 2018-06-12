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
    public class PurchaseEntriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public PurchaseEntriesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
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
                var purchaseEntry = await _db.PurchaseEntries.Where(p => p.RestaurantId == restaurant)
                    .ToListAsync();

                return View(purchaseEntry);
            }
        }

        #endregion

        #region Create

        //GET:
        [HttpGet]
        public IActionResult Create()
        {
            var purchaseEntry = new PurchaseEntry();
            return PartialView("Create", purchaseEntry);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseEntry purchaseEntry)
        {
            var restaurant = _session.GetInt32("restaurantsessionid");
            if (ModelState.IsValid)
            {
                if (restaurant != null)
                {
                    purchaseEntry.RestaurantId = Convert.ToInt32(restaurant);

                    await _db.AddAsync(purchaseEntry);
                    await _db.SaveChangesAsync();

                    TempData["purchaseentry"] = "You have successfully added " + purchaseEntry.Name + " as a new Purchase Entry!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();

                    return Json(new { success = true });
                }
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit

        //GET: PurchaseEntries/Edit/6
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseEntry = await _db.PurchaseEntries.SingleOrDefaultAsync(p => p.PurchaseEntryId == id);

            if (purchaseEntry == null)
            {
                return NotFound();
            }

            return PartialView("Edit", purchaseEntry);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, PurchaseEntry purchaseEntry)
        {
            if (id != purchaseEntry.PurchaseEntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var restaurant = _session.GetInt32("restaurantsessionid");

                if (restaurant != null)
                {
                    purchaseEntry.RestaurantId = Convert.ToInt32(restaurant);

                    _db.Update(purchaseEntry);
                    await _db.SaveChangesAsync();

                    return Json(new { success = true });
                }
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        //GET: PurchaseEntries/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseEntry = await _db.PurchaseEntries.SingleOrDefaultAsync(b => b.PurchaseEntryId == id);

            if (purchaseEntry == null)
            {
                return NotFound();
            }

            return PartialView("Delete", purchaseEntry);
        }

        //POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseEntry = await _db.PurchaseEntries.SingleOrDefaultAsync(b => b.PurchaseEntryId == id);
            if (purchaseEntry != null)
            {
                _db.PurchaseEntries.Remove(purchaseEntry);
                await _db.SaveChangesAsync();

                TempData["purchaseentry"] = "You have successfully deleted " + purchaseEntry.Name + " Purchase Entry!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Purchase Entries Exists

        private bool PurchaseEntriesExists(int id)
        {
            return _db.PurchaseEntries.Any(p => p.PurchaseEntryId == id);
        }

        #endregion

    }
}
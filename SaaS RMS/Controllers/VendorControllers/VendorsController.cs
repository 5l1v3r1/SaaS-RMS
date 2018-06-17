using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Vendor;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.VendorControllers
{
    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public VendorsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            var vendor = _db.Vendors.Where(v => v.RestaurantId == restaurant)
                .Include(v => v.Restuarant);

            if(vendor != null)
            {
                return View(await vendor.ToListAsync());
            }
            else
            {
                return RedirectToAction("Access", "Restaurants");
            }
        }

        #endregion

        #region Create

        //GET: Vendors/Create
        [HttpGet]
        public IActionResult Create()
        {
            var vendor = new Vendor();
            return PartialView("Create", vendor);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vendor vendor)
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            if (ModelState.IsValid)
            {
                if (restaurant != null)
                {
                    vendor.RestaurantId = restaurant;

                    var allVendors = await _db.Vendors.Where(v => v.RestaurantId == restaurant).ToListAsync();

                    if (allVendors.Any(v => v.Name == vendor.Name))
                    {
                        TempData["vendor"] = "You cannot add " + vendor.Name + " Vendor because it already exist!!!";
                        TempData["notificationType"] = NotificationType.Error.ToString();
                        return RedirectToAction("Index");
                    }

                    

                    await _db.AddAsync(vendor);
                    await _db.SaveChangesAsync();

                    TempData["vendor"] = "You have successfully added " + vendor.Name + " as a new Vendor!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();

                    return Json(new { success = true });
                }
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Edit

        //GET: Vendors/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _db.Vendors.SingleOrDefaultAsync(v => v.VendorId == id);

            if (vendor == null)
            {
                return NotFound();
            }

            return PartialView("Edit", vendor);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Vendor vendor)
        {
            if (id != vendor.VendorId)
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
                        vendor.RestaurantId = restaurant;

                        _db.Update(vendor);
                        await _db.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.VendorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["vendor"] = "You have successfully modified " + vendor.Name + " Vendor!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        //GET: Vendors/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _db.Vendors.SingleOrDefaultAsync(b => b.VendorId == id);

            if (vendor == null)
            {
                return NotFound();
            }

            return PartialView("Delete", vendor);
        }

        //POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendor = await _db.Vendors.SingleOrDefaultAsync(b => b.VendorId == id);
            if (vendor != null)
            {
                _db.Vendors.Remove(vendor);
                await _db.SaveChangesAsync();

                TempData["vendor"] = "You have successfully deleted " + vendor.Name + " Vendor!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion
        
        #region Vendor Exists

        private bool VendorExists(int id)
        {
            return _db.Vendors.Any(b => b.VendorId == id);
        }

        #endregion

    }
}
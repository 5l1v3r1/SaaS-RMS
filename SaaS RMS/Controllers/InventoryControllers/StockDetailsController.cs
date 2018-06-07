using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Inventory;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.InventoryControllers
{
    public class StockDetailsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constuctor

        public StockDetailsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Fetch Data

        public JsonResult GetProductsForCategory(int id)
        {
            var restaurant = _session.GetInt32("restaurantsessionid");
            var products = _db.Products.Where(p => p.Category.RestaurantId == restaurant && p.CategoryId == id );
            return Json(products);
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            var stockDetails = await _db.StockDetails.Where(s => s.RestaurantId == restaurant)
                .Include(s => s.Restuarant)
                .Include(s => s.Vendor)
                .Include(s => s.Product)
                .ToListAsync();

            if (stockDetails != null)
            {
                return View(stockDetails);
            }
            if (restaurant == null)
            {
                return RedirectToAction("Access", "Restaurants");
            }
            
            return View();
        }

        #endregion

        #region Create

        //GET: StockDetails/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            var categories = await _db.Categories.Where(l => l.RestaurantId == restaurant).ToListAsync();
            var vendors = await _db.Vendors.Where(v => v.RestaurantId == restaurant).ToListAsync();

            ViewBag.Category = new SelectList(categories, "CategoryId", "Name");
            ViewBag.VendorId = new SelectList(vendors, "VendorId", "Name");

            var stockDetail = new StockDetail();
            return View(stockDetail);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockDetail stockDetail)
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            if (ModelState.IsValid)
            {
                var allStockDetails = await _db.StockDetails.Where(s => s.RestaurantId == restaurant).ToListAsync();
                //allStockDetails.Find(s => s.ProductId == stockDetail.ProductId);
                
                if (allStockDetails.Any(s => s.ProductId == stockDetail.ProductId))
                {
                    
                    TempData["stockdetail"] = "You cannot add the Stock Detail because it already exist!!!";
                    TempData["notificationType"] = NotificationType.Error.ToString();
                    return RedirectToAction("Index");
                }
                if (restaurant != null)
                {
                    stockDetail.RestaurantId = Convert.ToInt32(restaurant);
                }
                await _db.AddAsync(stockDetail);
                await _db.SaveChangesAsync();

                var productName = _db.Products.Find(stockDetail.ProductId);

                TempData["stockDetail"] = "You have successfully added a Stock Detail for Product "+ productName.Name +" !!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return RedirectToAction("Index");
            }

            
            var vendors = await _db.Vendors.Where(v => v.RestaurantId == restaurant).ToListAsync();
            
            ViewBag.VendorId = new SelectList(vendors, "VendorId", "Name", stockDetail.VendorId);
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        //GET: StockDetails/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockDetail = await _db.StockDetails.SingleOrDefaultAsync(b => b.StockDetailId == id);

            if (stockDetail == null)
            {
                return NotFound();
            }

            return PartialView("Delete", stockDetail);
        }

        //POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockDetail = await _db.StockDetails.SingleOrDefaultAsync(b => b.StockDetailId == id);
            if (stockDetail != null)
            {
                _db.StockDetails.Remove(stockDetail);
                await _db.SaveChangesAsync();

                TempData["stockDetail"] = "You have successfully deleted " + stockDetail.Product + " Stock Detail!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit

        //GET: StockDetails/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockDetail = await _db.StockDetails.SingleOrDefaultAsync(p => p.StockDetailId == id);
            var restaurant = _session.GetInt32("restaurantsessionid");

            var categories = await _db.Categories.Where(l => l.RestaurantId == restaurant).ToListAsync();
            var vendors = await _db.Vendors.Where(v => v.RestaurantId == restaurant).ToListAsync();

            ViewBag.Category = new SelectList(categories, "CategoryId", "Name");
            ViewBag.VendorId = new SelectList(vendors, "VendorId", "Name");

            if (stockDetail == null)
            {
                return NotFound();
            }

            return View(stockDetail);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, StockDetail stockDetail)
        {
            if (id != stockDetail.StockDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var restaurant = _session.GetInt32("restaurantsessionid");
                
                if (restaurant != null)
                {
                    stockDetail.RestaurantId = Convert.ToInt32(restaurant);
                }
                _db.Update(stockDetail);
                await _db.SaveChangesAsync();

                var productName = _db.Products.Find(stockDetail.ProductId);

                TempData["stockDetail"] = "You have successfully modified a Stock Detail for a Product !!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        #endregion


        #region StockDetials Exists

        private bool StockDetailsExists(int? id)
        {
            return _db.StockDetails.Any(s => s.StockDetailId == id);
        }

        #endregion
        
    }
}
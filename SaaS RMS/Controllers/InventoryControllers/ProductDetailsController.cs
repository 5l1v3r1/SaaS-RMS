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
    public class ProductDetailsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constuctor

        public ProductDetailsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
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

            var productDetails = await _db.ProductDetails.Where(s => s.RestaurantId == restaurant)
                .Include(s => s.Restuarant)
                .Include(s => s.Vendor)
                .Include(s => s.Product)
                .ToListAsync();

            if (restaurant != null)
            {
                return View(productDetails);
            }
            if (restaurant == null)
            {
                return RedirectToAction("Access", "Restaurants");
            }
            
            return View();
        }

        #endregion

        #region Create

        //GET: ProductDetails/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            var categories = await _db.Categories.Where(l => l.RestaurantId == restaurant).ToListAsync();
            var vendors = await _db.Vendors.Where(v => v.RestaurantId == restaurant).ToListAsync();

            ViewBag.Category = new SelectList(categories, "CategoryId", "Name");
            ViewBag.VendorId = new SelectList(vendors, "VendorId", "Name");

            var productDetail = new ProductDetail();
            return View(productDetail);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDetail productDetail)
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            if (ModelState.IsValid)
            {
                var allProductDetails = await _db.ProductDetails.Where(s => s.RestaurantId == restaurant).ToListAsync();
                
                if (allProductDetails.Any(s => s.ProductId == productDetail.ProductId))
                {
                    
                    TempData["productdetail"] = "You cannot add the Product Detail because it already exist!!!";
                    TempData["notificationType"] = NotificationType.Error.ToString();
                    return RedirectToAction("Index");
                }
                if (restaurant != null)
                {
                    productDetail.RestaurantId = Convert.ToInt32(restaurant);
                }
                await _db.AddAsync(productDetail);
                await _db.SaveChangesAsync();

                var productName = _db.Products.Find(productDetail.ProductId);
                 
                TempData["productDetail"] = "You have successfully added a Product Detail for Product "+ productName.Name +" !!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return RedirectToAction("Index");
            }

            
            var vendors = await _db.Vendors.Where(v => v.RestaurantId == restaurant).ToListAsync();
            
            ViewBag.VendorId = new SelectList(vendors, "VendorId", "Name", productDetail.VendorId);
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        //GET: ProductDetails/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDetail = await _db.ProductDetails.SingleOrDefaultAsync(b => b.ProductDetailId == id);

            if (productDetail == null)
            {
                return NotFound();
            }

            return PartialView("Delete", productDetail);
        }

        //POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productDetail = await _db.ProductDetails.SingleOrDefaultAsync(b => b.ProductDetailId == id);
            if (productDetail != null)
            {
                _db.ProductDetails.Remove(productDetail);
                await _db.SaveChangesAsync();

                TempData["productDetail"] = "You have successfully deleted " + productDetail.Product + " Product Detail!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit

        //GET: ProductDetails/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDetail = await _db.ProductDetails.SingleOrDefaultAsync(p => p.ProductDetailId == id);
            var restaurant = _session.GetInt32("restaurantsessionid");

            var categories = await _db.Categories.Where(l => l.RestaurantId == restaurant).ToListAsync();
            var vendors = await _db.Vendors.Where(v => v.RestaurantId == restaurant).ToListAsync();

            ViewBag.Category = new SelectList(categories, "CategoryId", "Name");
            ViewBag.VendorId = new SelectList(vendors, "VendorId", "Name");

            if (productDetail == null)
            {
                return NotFound();
            }

            return View(productDetail);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductDetail productDetail)
        {
            if (id != productDetail.ProductDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var restaurant = _session.GetInt32("restaurantsessionid");
                
                if (restaurant != null)
                {
                    productDetail.RestaurantId = Convert.ToInt32(restaurant);
                }
                _db.Update(productDetail);
                await _db.SaveChangesAsync();

                var productName = _db.Products.Find(productDetail.ProductId);

                TempData["productDetail"] = "You have successfully modified a Product Detail for a Product !!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        #endregion
        
        #region ProductDetials Exists

        private bool ProductDetailsExists(int? id)
        {
            return _db.ProductDetails.Any(s => s.ProductDetailId == id);
        }

        #endregion
        
    }
}
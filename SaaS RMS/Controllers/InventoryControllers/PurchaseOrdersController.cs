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
    public class PurchaseOrdersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public PurchaseOrdersController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Index

        [Route("purchaseOrders/index/{OrderEntryId}")]
        public async Task<IActionResult> Index(int OrderEntryId)
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            if (restaurant == null)
            {
                return RedirectToAction("Access", "Restaurants");
            }

            var purchaseOrder = await _db.PurchaseOrders.Include(p => p.ProductDetail)
                       .Include(p => p.OrderEntry)
                       .ToListAsync();

            if (purchaseOrder == null)
            {
                return NotFound();
            }
            else
            {
                _session.SetInt32("orderentrysessionid", OrderEntryId);
            }

            return View(purchaseOrder);
        }

        #endregion

        #region Create

        //GET: PurchaseOrder/Create
        [HttpGet]
        public IActionResult Create()
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            var productDetails = _db.ProductDetails.Where(s => s.RestaurantId == restaurant).ToArray();
            

            var length = productDetails.Length;
            List<SelectListItem> items = new List<SelectListItem>();
            for (int i = 0; i < length; i++)
            {
                var product = _db.Products.Where(s => s.ProductId == productDetails[i].ProductId).SingleOrDefault();
                var name = product.Name;
                items.Add(new SelectListItem
                {
                    Text = name,
                    Value = productDetails[i].ProductDetailId.ToString()
                });
            }

            ViewBag.ProductDetails = items;
            var purchase = new PurchaseOrder();
            return View(purchase);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseOrder purchase)
        {
            var purchaseentryid = _session.GetInt32("orderentrysessionid");
            var restaurant = _session.GetInt32("restaurantsessionid");
            var productdetails = _db.ProductDetails.Where(s => s.RestaurantId == restaurant);

            if (ModelState.IsValid)
            {
                if (purchaseentryid != null)
                {
                    purchase.PurchaseEntryId = Convert.ToInt32(purchaseentryid);

                    await _db.AddAsync(purchase);
                    await _db.SaveChangesAsync();

                    TempData["purchase"] = "You have successfully added " + purchase.ProductDetail.Product.Name + " as a new Product!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();

                    return Json(new { success = true });
                }
            }
            ViewBag.ProductDetail = new SelectList(productdetails, "ProductDetaillId", "Product.Name");
            return RedirectToAction("Index", new { PurchaseEntryId = purchaseentryid });
        }

        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _db.PurchaseOrders.SingleOrDefaultAsync(p => p.PurchaseOrderId == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return PartialView(purchaseOrder);
        }

        #endregion

        #region Fetech Data

        public JsonResult GetAmountForProduct(int id)
        {
            var restaurant = _session.GetInt32("restaurantsessionid");
            var productDetail = _db.ProductDetails.Where(s => s.ProductDetailId == id && s.RestaurantId == restaurant);

            var amount = productDetail.Any(s => s.Amount > 0);
            return Json(amount);
        }

        #endregion

        #region PurchaseOrders Exists

        private bool PurchaseExists(int? id)
        {
            return _db.PurchaseOrders.Any(p => p.PurchaseEntryId == id);
        }

        #endregion

    }
}
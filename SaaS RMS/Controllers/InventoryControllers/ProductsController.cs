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
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public ProductsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Index

        [Route("products/index/{CategoryId}")]
        public async Task<IActionResult> Index(int CategoryId)
        {
            try
            {
                var product = await _db.Products.Where(p => p.CategoryId == CategoryId)
                    .Include(p => p.Category)
                    .ToListAsync();

                if (product != null)
                {
                    _session.SetInt32("categorysessionid", CategoryId);
                    return View(product);
                }
            }
            catch(Exception e)
            {
                return Json(e);
            }
            return View();
        }

        #endregion

        #region Create

        //GET: Products/Create
        [HttpGet]
        public IActionResult Create()
        {
            var product = new Product();
            return PartialView("Create", product);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            var categorysessionid = _session.GetInt32("categorysessionid");

            if (ModelState.IsValid)
            {
                var allProducts = await _db.Products.Where(p => p.CategoryId == categorysessionid).ToListAsync();
                if (allProducts.Any(p => p.Name == product.Name))
                {
                    TempData["product"] = "You cannot add " + product.Name + " Product because it already exist!!!";
                    TempData["notificationType"] = NotificationType.Error.ToString();
                    return RedirectToAction("Index", new { CategoryId = categorysessionid });
                }

                if (categorysessionid != null)
                {
                    product.CategoryId = Convert.ToInt32(categorysessionid);
                }

                await _db.AddAsync(product);
                await _db.SaveChangesAsync();

                TempData["product"] = "You have successfully added " + product.Name + " as a new Product!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();
                    
                return Json(new { success = true });
            }

            return RedirectToAction("Index", new { CategoryId = categorysessionid });
        }

        #endregion

        #region Edit

        //GET: Products/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Products.SingleOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return PartialView("Edit", product);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Product product)
        {
            var categorysessionid = _session.GetInt32("categorysessionid");

            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (categorysessionid != null)
                    {
                        product.CategoryId = Convert.ToInt32(categorysessionid);

                        _db.Update(product);
                        await _db.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["product"] = "You have successfully modified " + product.Name + " Product!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index", new { CategoryId = categorysessionid });
        }

        #endregion

        #region Delete

        //GET: Products/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Products.SingleOrDefaultAsync(b => b.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return PartialView("Delete", product);
        }

        //POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categorysessionid = _session.GetInt32("categorysessionid");
            var product = await _db.Products.SingleOrDefaultAsync(b => b.ProductId == id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();

                TempData["product"] = "You have successfully deleted " + product.Name + " Product!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index", new { CategoryId = categorysessionid });
        }

        #endregion

        #region Product Exists

        private bool ProductExists(int id)
        {
            return _db.Products.Any(p => p.CategoryId == id);
        }

        #endregion

    }
}
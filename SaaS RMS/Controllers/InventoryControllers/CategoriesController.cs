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
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public CategoriesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            if (restaurant != null)
            {
                var category = await _db.Categories.Where(c => c.RestaurantId == restaurant)
                    .Include(c => c.Restaurant)
                    .ToListAsync();

                return View(category);
            }
            else
            {
                return RedirectToAction("Access", "Restaurants");
            }
        }

        #endregion

        #region Create

        //GET: Categories/Create
        [HttpGet]
        public IActionResult Create()
        {
            var category = new Category();
            return PartialView("Create", category);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            if (ModelState.IsValid)
            {
                if (restaurant!= null)
                {
                    category.RestaurantId = restaurant;

                    var allCategories = _db.Categories.Where(c => c.RestaurantId == restaurant);

                    if(allCategories.Any(c => c.Name == category.Name))
                    {
                        TempData["category"] = "You cannot add " + category.Name + " Category because it already exist!!!";
                        TempData["notificationType"] = NotificationType.Error.ToString();
                        return RedirectToAction("Index");
                    }

                    await _db.AddAsync(category);
                    await _db.SaveChangesAsync();

                    TempData["category"] = "You have successfully added " + category.Name + " as a new Category!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();

                    return Json(new { success = true });
                }
            }
            return RedirectToAction("Access", "Restaurants");
        }

        #endregion

        #region Edit

        //GET: Categories/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var category = await _db.Categories.SingleOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return PartialView("Edit", category);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var restaurant = _session.GetInt32("restaurantsessionid");

                    if(restaurant != null)
                    {
                        category.RestaurantId = restaurant;

                        _db.Update(category);
                        await _db.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["category"] = "You have successfully modified " + category.Name + " Category!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        //GET: Categories/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _db.Categories.SingleOrDefaultAsync(b => b.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            return PartialView("Delete", category);
        }

        //POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _db.Categories.SingleOrDefaultAsync(b => b.CategoryId == id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();

                TempData["category"] = "You have successfully deleted " + category.Name + " Category!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Category Exists

        private bool CategoryExists(int id)
        {
            return _db.Categories.Any(b => b.CategoryId == id);
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.RestaurantController
{
    public class DishesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IHostingEnvironment _environment;

        #region Constructor

        public DishesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IHostingEnvironment environment)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }

        #endregion

        #region Dishes Index

        public async Task<IActionResult> Index(int? id)
        {
            _session.SetInt32("MealId", Convert.ToInt32(id));

            if (id == null)
            {
                return NotFound();
            }

            var dish = _db.Dishes.Include(d => d.Meal).ToListAsync();

            if (dish == null)
            {
                return NotFound();
            }

            return View(await dish);
        }

        #endregion

        #region Dishes Create

        //GET: Dishes/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dish dish, IFormFile file, UploadType uploadType)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("null_img", "Image file not selected!!!");
            }

            else
            {
                var fileinfo = new FileInfo(file.FileName);
                var filename = DateTime.Now.ToFileTime() + fileinfo.Extension;
                var uploads = Path.Combine(_environment.WebRootPath, "uploads" + uploadType);

                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }

                if (ModelState.IsValid)
                {
                    var meal = _session.GetInt32("MealId");

                    if (meal != null)
                    {
                        dish.MealId = Convert.ToInt32(meal);
                    }

                    dish.Image = filename;
                    await _db.Dishes.AddAsync(dish);
                    await _db.SaveChangesAsync();

                    TempData["dish"] = "You have successfully added a new dish!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Dishes Edit

        //GET: Dishes/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _db.Dishes.SingleOrDefaultAsync(m => m.DishId == id);

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, IFormFile file, Dish dish, UploadType uploadType)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("null_img", "Image file not selected!!!");
            }

            else
            {
                var fileinfo = new FileInfo(file.FileName);
                var filename = DateTime.Now.ToFileTime() + fileinfo.Extension;
                var uploads = Path.Combine(_environment.WebRootPath, "uploads" + uploadType);

                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var meal = _session.GetInt32("MealId");

                        if (meal != null)
                        {
                            dish.MealId = Convert.ToInt32(meal);
                        }

                        dish.Image = filename;
                        _db.Update(dish);
                        await _db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!DishExists(dish.DishId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    TempData["dish"] = " You have successfully modified a Dish";
                    TempData["notificationType"] = NotificationType.Success.ToString();
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Dishes Delete

        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _db.Dishes
                .Include(l => l.Meal)
                .SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return PartialView("Delete", dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _db.Dishes.SingleOrDefaultAsync(m => m.DishId == id);
            if (dish != null)
            {
                _db.Dishes.Remove(dish);
                await _db.SaveChangesAsync();

                TempData["dish"] = "You have successfully deleted a Dish!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Dish Exists

        private bool DishExists(int id)
        {
            return _db.Dishes.Any(e => e.DishId == id);
        }

        #endregion

    }
}
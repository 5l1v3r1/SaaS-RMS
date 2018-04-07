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

        #region Index

        [Route("dishes/index/{MealId}")]
        public async Task<IActionResult> Index(int? MealId)
        {
            try
            {
                var dish = _db.Dishes.Where(d => d.MealId == MealId);
                if (dish != null)
                {
                    _session.SetInt32("mealsessionid", Convert.ToInt32(MealId));
                    return View(await dish.ToListAsync());
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

        //GET: Dishes/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, Dish dish, UploadType uploadType)
        {
            var mealsessionid = _session.GetInt32("mealsessionid");

            if (file == null || file.Length == 0)
            {
                ViewData["null_image"] = "Please select an image";
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
                    var allDishes = await _db.Dishes.ToListAsync();

                    if (allDishes.Any(d => d.Name == dish.Name && d.MealId == mealsessionid))
                    {
                        TempData["dish"] = "You cannot add " + dish.Name + " dish because it already exist!!!";
                        TempData["notificationType"] = NotificationType.Error.ToString();
                        return RedirectToAction("Index", new { MealId = mealsessionid });
                    }

                    if (mealsessionid != null)
                    {
                        dish.MealId = Convert.ToInt32(mealsessionid);
                    }

                    dish.Image = filename;
                    _db.Dishes.Add(dish);
                    await _db.SaveChangesAsync();

                    TempData["dish"] = "You have successfully added " + dish.Name + " as a new Dish!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();

                    return RedirectToAction("Index", new { MealId = mealsessionid });
                }
            }
            return RedirectToAction("Index", new { MealId = mealsessionid });
        }

        #endregion

        #region Edit

        //GET: Dishes/Edit/4
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _db.Dishes.SingleOrDefaultAsync(m => m.DishId == id);

            ViewData["DishName"] = dish.Name;

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int? id, UploadType uploadType, Dish dish)
        {
            var mealsessionid = _session.GetInt32("mealsessionid");

            if (id != dish.DishId)
            {
                return NotFound();
            }

            if (file == null || file.Length == 0)
            {
                ViewData["null_image"] = "Please select an image";
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
                    var allDishes = await _db.Dishes.ToListAsync();

                    if (allDishes.Any(d => d.Name == dish.Name && d.MealId == mealsessionid))
                    {
                        TempData["dish"] = "You cannot modify " + dish.Name + " dish because it already exist!!!";
                        TempData["notificationType"] = NotificationType.Error.ToString();
                        return RedirectToAction("Index", new { MealId = mealsessionid });
                    }

                    if (mealsessionid != null)
                    {
                        dish.MealId = Convert.ToInt32(mealsessionid);
                    }

                    try
                    {
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

                    TempData["dish"] = "You have successfully modified " + dish.Name + " dish!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();
                    return RedirectToAction("Index", new { MealId = mealsessionid });
                }
            }
            return RedirectToAction("Index", new { MealId = mealsessionid });
        }

        #endregion

        #region Delete

        // GET: Dishes/Delete/5
        [HttpGet]
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
            var mealsessionid = _session.GetInt32("mealsessionid");
            var dish = await _db.Dishes.SingleOrDefaultAsync(m => m.DishId == id);
            ViewData["dishname"] = dish.Name;
            if (dish != null)
            {
                _db.Dishes.Remove(dish);
                await _db.SaveChangesAsync();

                TempData["dish"] = "You have successfully deleted " + dish.Name + " dish!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index", new { MealId = mealsessionid });
        }

        #endregion

        #region Picture

        public IActionResult Picture()
        {
            var dish = new Dish();
            return PartialView("Picture", dish);
        }

        #endregion

        #region DishExists

        private bool DishExists(int id)
        {
            return _db.Dishes.Any(e => e.DishId == id);
        }

        #endregion

    }
}
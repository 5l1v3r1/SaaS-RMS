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
    public class MealsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IHostingEnvironment _environment;

        #region Constructor

        public MealsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IHostingEnvironment environment)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            var restaurant = _session.GetInt32("RId");
            
            if (restaurant == null)
            {
                return RedirectToAction("Access", "Restaurants");
            }

            var meal = await _db.Meals.Where(m => m.RestaurantId == restaurant)
                .Include(m => m.Restaurant)
                .ToListAsync();


            if ( meal != null)
            {
                return View(meal);
            }

            return View();
        }

        #endregion

        #region Create

        //GET: Meals/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Meal meal, IFormFile file, UploadType uploadType)
        {
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
                    var restaurant = _session.GetInt32("RId");
                    var allMeals = await _db.Meals.ToListAsync();

                    if (allMeals.Any(m => m.Name == meal.Name))
                    {
                        TempData["meal"] = "You cannot add " + meal.Name + " meal because it already exist!!!";
                        TempData["notificationType"] = NotificationType.Error.ToString();
                        return RedirectToAction("Index", meal);
                    }

                    if (restaurant == null)
                    {
                        return RedirectToAction("Access", "Restaurant");
                    }
                    else
                    {
                        meal.RestaurantId = Convert.ToInt32(restaurant);
                    }


                    meal.Image = filename;
                    _db.Meals.Add(meal);
                    await _db.SaveChangesAsync();

                    TempData["meal"] = "You have successfully added " + meal.Name  + " as a new Meal!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();
                }
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Edit

        //GET: Meals/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _db.Meals.SingleOrDefaultAsync(m => m.MealId == id);
            ViewData["MealName"] = meal.Name;
            
            if (meal == null)
            {
                return NotFound();
            }

            return View( meal);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Meal meal, IFormFile file, UploadType uploadType, int? id)
        {
            if (id != meal.MealId)
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
                    var restaurant = _session.GetInt32("RId");

                    if (restaurant != null)
                    {
                        meal.RestaurantId = Convert.ToInt32(restaurant);
                    }

                    try
                    {
                        meal.Image = filename;
                        _db.Update(meal);
                        await _db.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MealExists(meal.MealId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    TempData["meal"] = "You have successfully modified " + meal.Name + " meal!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();
                    return RedirectToAction("Index");

                }
            }
            return RedirectToAction("Index");
            
        }

        #endregion

        #region Delete

        // GET: Meals/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _db.Meals
                .Include(l => l.Restaurant)
                .SingleOrDefaultAsync(m => m.MealId == id);
            if (meal == null)
            {
                return NotFound();
            }
            return PartialView("Delete", meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meal = await _db.Meals.SingleOrDefaultAsync(m => m.MealId == id);
            if (meal != null)
            {
                _db.Meals.Remove(meal);
                await _db.SaveChangesAsync();

                TempData["meal"] = "You have successfully deleted " + meal.Name + " Meal!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Picture

        public IActionResult Picture()
        {
            var meal = new Meal();
            return PartialView("Picture", meal);
        }

        #endregion

        #region MealExits

        private bool MealExists(int id)
        {
            return _db.Meals.Any(e => e.MealId == id);
        }

        #endregion
    }
}
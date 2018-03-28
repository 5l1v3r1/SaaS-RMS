using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Enums;
using SaaS_RMS.Services;

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

        #region Meal Index

        public async Task<IActionResult> Index()
        {
            var restaurant = _session.GetInt32("RId");

            if(restaurant == null)
            {
                return RedirectToAction("Access", "Restaurants");
            }

            var meals = _db.Meals.Where(m => m.RestaurantId == restaurant)
                .Include(m => m.Restaurant)
                .ToListAsync();

            if (meals != null)
            {
                return View(await meals);
            }
            else
            {
                return RedirectToAction("Access", "Restaurants");
            }
        }

        #endregion

        #region Meal Create

        //GET: Meals/Create
        [HttpGet]
        public IActionResult Create()
        {
            var meal = new Meal();
            return PartialView("Create", meal);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Meal meals, IFormFile file)
        {
            var restaurant = _session.GetInt32("RId");

            if (ModelState.IsValid)
            {
                if (restaurant != null)
                {
                    meals.RestaurantId = Convert.ToInt32(restaurant);
                    if (file != null && file.Length > 0)
                    {
                        new FileUploader().UploadFile(file, UploadType.Food);
                    }

                    var allMeals = await _db.Meals.Where(m => m.RestaurantId == restaurant).ToListAsync();
                    if(allMeals.Any(m => m.Name == meals.Name))
                    {
                        TempData["meal"] = "You cannot add this Meal because it already exist!!!";
                        TempData["notificationType"] = NotificationType.Error.ToString();
                        return RedirectToAction("Index");
                    }

                    await _db.AddAsync(meals);
                    await _db.SaveChangesAsync();

                    TempData["meal"] = "You have successfully added a new Meal!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();

                    return Json(new { success = true });
                }
                else
                {
                    TempData["meal"] = "Session Expired, Login Again";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Restaurant", "Access");
                }
            }
            return View("Index");
        }

        #endregion
    }
}
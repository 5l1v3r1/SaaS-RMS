using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SaaS_RMS.Models.Entities.Landing;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models;

namespace SaaS_RMS.Controllers.SystemControllers
{
    public class RestaurantsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public RestaurantsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Fetch Data

        public JsonResult GetLgasForState(int id)
        {
            var lgas = _db.Lgas.Where(l => l.StateId == id);
            return Json(lgas);
        }

        #endregion

        #region Restaurant Index

        public async Task<IActionResult> Index()
        {
            return View(await _db.Restaurants.ToListAsync());
        }

        #endregion

        #region Restaurant Register

        // GET: /Restaurant/Register
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.StateId = new SelectList(_db.States, "StateId", "Name");
            return View();
        }

        // POST: /Restaurant/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                if (await _db.Restaurants.AnyAsync(r => r.Name == restaurant.Name))
                {
                    ModelState.AddModelError("", "Restaurant name already exists");
                }

                if (await _db.Restaurants.AnyAsync(r => r.ContactEmail == restaurant.ContactEmail))
                {
                    ModelState.AddModelError("", "Contact Email already exists");
                }

                else
                {
                    var _restaurant = new Restaurant
                    {
                        Name = restaurant.Name,
                        Motto = restaurant.Motto,
                        ContactEmail = restaurant.ContactEmail,
                        ContactNumber = restaurant.ContactNumber,
                        AccessCode = restaurant.AccessCode,
                        LgaId = restaurant.LgaId,
                        Location = restaurant.Location,
                        Status = RestaurantStatus.Inactive
                        
                    };

                    _db.Restaurants.Add(_restaurant);
                    await _db.SaveChangesAsync();

                    TempData["Success"] = restaurant.Name + " have successfully joined the Odarms community";
                    TempData["notificationType"] = NotificationType.Success.ToString();
                    return Json(new { success = true });
                }

            }
            return View();
        }

        #endregion

        #region Restaurant Subscription

        public IActionResult Subscription()
        {
            return View();
        }

        #endregion

        #region Restaurant Access

        //GET: Restaurant/Access
        [HttpGet]
        public IActionResult Access()
        {
            return View();
        }

        //POST: 
        [HttpPost]
        public async Task<IActionResult> Access(Restaurant restaurant)
        {
            //int restaurantId;

            //var rest = _db.Restaurants.SingleAsync(r => r.Name == restaurant.Name && r.AccessCode == restaurant.AccessCode);
            var rest = await _db.Restaurants.FirstOrDefaultAsync(r => r.Name == restaurant.Name && r.AccessCode == restaurant.AccessCode);
            if (rest != null)
            {
                return RedirectToAction("Admin", new { ID = rest.RestaurantId });
            }
            else
            {
                ViewData["mismatch"] = "Restaurant name doesn't match the access code";
            }

            return View();
        }
        #endregion

        #region Restaurant Admin

        public IActionResult Admin(int? ID)
        {
            int _ID = Convert.ToInt32(ID);
            _session.SetInt32("restaurantsessionid", _ID);
            ViewData["restaurantid"] = _session.GetInt32("restaurantsessionid");
            return View();
        }

        #endregion

        #region Restaurant Profile

        // GET: Restaurants/Profile/5
        public async Task<IActionResult> Profile(int? id)
        {
            ViewData["restaurantid"] = _session.GetInt32("restaurantsessionid");

            if (id == null)
            {
                return RedirectToAction("Access", "Restaurants");
            }

            var restaurant = await _db.Restaurants
                .Include(r => r.Lga)
                .SingleOrDefaultAsync(m => m.RestaurantId == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        #endregion

        #region Restaurant Edit Profile

        //GET: Restaurants/Settings
        [HttpGet]
        public async Task<IActionResult> EditProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _db.Restaurants.SingleOrDefaultAsync(s => s.RestaurantId == id);

            if (restaurant == null)
            {
                return NotFound();
            }
            ViewBag.StateId = new SelectList(_db.States, "StateId", "Name");
            return View(restaurant);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfile(int? id, Restaurant restaurant)
        {
            var ID = id;
            if (id != restaurant.RestaurantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(restaurant);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.RestaurantId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["restaurant"] = "You have successfully modified " + restaurant.Name + " Restaurant!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return RedirectToAction("Profile", new { id = ID });
            }
            return RedirectToAction("Profile");
        }

        #endregion

        #region Restaurant Dashborad

        [HttpGet]
        public IActionResult Dashboard(int? id)
        {
            if (id != null)
            {
                var meals = _db.Meals.Where(m => m.RestaurantId == id);
                List<MealDishResponse> response = new List<MealDishResponse>();
                var landingInfo = _db.LandingInfo.Where(l => l.Approval == ApprovalEnum.Apply);
                var restaurant = _db.Restaurants.Find(id);

                ViewData["restaurantname"] = restaurant.Name;
                ViewData["restaurantlogo"] = restaurant.Logo;
                ViewData["restaurantmotto"] = restaurant.Motto;
                ViewData["restaurantlocation"] = restaurant.Location;
                ViewData["restaurantnumber"] = restaurant.ContactNumber;
                ViewData["restaurantemail"] = restaurant.ContactEmail;


                foreach (var meal in meals)
                {
                    var dishes = _db.Dishes.Where(s => s.MealId == meal.MealId).ToList();
                    var _response = new MealDishResponse
                    {
                        meal = meal,
                        dishes = dishes
                    };
                    response.Add(_response);
                }
                
                ViewData["dishes"] = response;
                
                return View();
            }

            else
            {
                return RedirectToAction("Dashboard", "Restaurants");
            }
        }

        [HttpGet]
        public JsonResult GetDishes(int? id)
        {
            if (id != null)
            {
                var meals = _db.Meals.Where(m => m.RestaurantId == id);
                List<MealDishResponse> response = new List<MealDishResponse>();

                foreach (var meal in meals)
                {
                    var dishes = _db.Dishes.Where(s => s.MealId == meal.MealId).ToList();
                    var _response = new MealDishResponse
                    {
                        meal = meal,
                        dishes = dishes
                    };
                    response.Add(_response);
                }
                return Json(new { data = response });
            }
            return Json(new { });
        }

        #endregion

        //[HttpGet]
        //public ActionResult Dashboard(int? id)
        //{
        //    _session.SetInt32("restaurantid", Convert.ToInt32(id));
        //    DashboardViewModel dVM = new DashboardViewModel();
        //    dVM.Restaurant = GetRestaurant();
        //    dVM.Meals = GetMeals();
        //    return View(dVM);
        //}

        //public Restaurant GetRestaurant()
        //{
        //    var rId = _session.GetInt32("restaurantid");
        //    var restaurant = _db.Restaurants.Where(r => r.RestaurantId == rId).ToList();

        //    return restaurant;
        //}

        //public Meal GetMeals()
        //{
        //    var rId = _session.GetInt32("restaurantid");
        //    var meals = _db.Meals.Where(m => m.RestaurantId == rId).ToList();

        //    return meals;
        //}

        #region Restaurant Exists

        private bool RestaurantExists(int id)
        {
            return _db.Restaurants.Any(e => e.RestaurantId == id);
        }

        #endregion

    }

    public class MealDishResponse{
        public Meal meal { get; set; }
        public List<Dish> dishes { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;

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


    }
}
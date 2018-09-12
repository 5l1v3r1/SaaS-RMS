using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public HomeController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion
        
        #region Landing

        public async Task<IActionResult> Landing()
        {
            var landinginfo = await _db.LandingInfo.SingleOrDefaultAsync(l => l.Approval == ApprovalEnum.Apply);

            return View(landinginfo);
        }

        #endregion

        #region Restaurants

        [HttpGet]
        public async Task<IActionResult> Restaurants()
        {
            var restaurantLength = _db.Restaurants.ToArray();

            var length = restaurantLength.Length;

            List<SelectListItem> items = new List<SelectListItem>();

            for (int i = 0; i < length; i++)
            {
                var restaurantid = _db.Restaurants.Where(id => id.RestaurantId == restaurantLength[i].RestaurantId).SingleOrDefault();
                var lgaid = restaurantid.LgaId;
                var stateid = _db.States.Find(lgaid);

                var name = stateid.Name;

                items.Add(new SelectListItem
                {
                    Text = name,
                    Value = stateid.ToString()
                });
            }
            ViewBag.StateId = items;
            var landinginfo = await _db.LandingInfo.SingleOrDefaultAsync(l => l.Approval == ApprovalEnum.Apply);
            return View(landinginfo);
        }

        #endregion

        #region Index

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Error

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Fetch Data

        public JsonResult GetLgasForState(int id)
        {
            var lgas = _db.Lgas.Where(l => l.StateId == id);
            return Json(lgas);
        }

        public JsonResult GetAllRestaurantsWithLGA(int id)
        {
            var allRestaurantWithLGA = _db.Restaurants.Where(r => r.LgaId == id);
            return Json(allRestaurantWithLGA);
        }

        
        
        
        #endregion

    }
}

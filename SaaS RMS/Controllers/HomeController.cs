using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models;
using SaaS_RMS.Models.Entities.Landing;
using SaaS_RMS.Models.Entities.System;
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

        public async Task<IActionResult> Restaurants()
        {
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

        #region Get Restaurants

        //GET: /Home/Restaurants
        [HttpGet]
        public IActionResult GetRestaurant()
        {
            ViewBag.StateId = new SelectList(_db.States, "StateId", "Name");
            ViewBag.Me = "king";
            return View();
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GetRestaurant(Restaurant restaurant)
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

        public JsonResult GetImageForLandingInfo()
        {
            var landinginfo = new LandingInfo();
            var approvedLandingInfo = _db.LandingInfo.Where(l => l.Approval == ApprovalEnum.Apply).ToList();
            return Json(approvedLandingInfo);
        }
        #endregion

    }
}

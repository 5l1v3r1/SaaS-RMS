using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Enities.Landing;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.LandingControllers
{
    public class LandingInfoController : Controller
    {
        private readonly ApplicationDbContext _db;

        #region Constructor

        public LandingInfoController(ApplicationDbContext context)
        {
            _db = context;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            var landingInfo = await _db.LandingInfo.ToListAsync();
            return View(landingInfo);
        }

        #endregion

        #region Create

        //GET: LandingInfo/Create
        [HttpGet]
        public IActionResult Create()
        {
            var landingInfo = new LandingInfo();
            return PartialView("Create", landingInfo);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LandingInfo landingInfo)
        {
            if (ModelState.IsValid)
            {
                await _db.AddAsync(landingInfo);
                await _db.SaveChangesAsync();

                TempData["landinginfo"] = "You have successfully added a new Landing Information!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }

        #endregion


    }
}
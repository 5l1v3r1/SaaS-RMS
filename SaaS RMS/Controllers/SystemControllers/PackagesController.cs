using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.SystemControllers
{
    public class PackagesController : Controller
    {
        private readonly ApplicationDbContext _db;

        #region Constructor

        public PackagesController(ApplicationDbContext context)
        {
            _db = context;
        }

        #endregion

        #region Package Index

        public IActionResult Index()
        {
            return View(_db.Restaurants.ToList());
        }

        #endregion

        #region Package Create

        //GET: Packages/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //POST: 
        [HttpPost]
        public IActionResult Create(Package package)
        {
            if (ModelState.IsValid)
            {
                var allPackages = _db.Packages.ToList();

                if (allPackages.Count >= 3)
                {
                    TempData["package"] = "You cannot have two packages with the same package type!";
                    TempData["notificationType"] = NotificationType.Error.ToString();
                    return RedirectToAction("Index");
                }

                if (allPackages.Any(p => p.Type == package.Type))
                {
                    TempData["package"] = "You cannot add this package because this type exist!";
                    TempData["notificationType"] = NotificationType.Error.ToString();
                    return RedirectToAction("Index");
                }

                _db.Packages.Add(package);
                _db.SaveChanges();
                TempData["package"] = "You have successfully added a new package!";
                TempData["notificationType"] = NotificationType.Success.ToString();
                return Json(new { success = true });
            }
            return View(package);
        }

        #endregion




    }
}
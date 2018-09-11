using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.SystemControllers
{
    public class PackagesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public PackagesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Package Index

        [Route("RMS/AllPackages")]
        public IActionResult Index()
        {
            var rmsadmin = _session.GetInt32("rmsloggedinuserid");

            if (rmsadmin == null)
            {
                return RedirectToAction("Login", "RMS_Admin");
            }

            var _userObject = _session.GetString("rmsloggedinuser");

            if (_userObject == null)
            {
                return RedirectToAction("Login", "RMS_Admin");
            }

            var _user = JsonConvert.DeserializeObject<RMSUser>(_userObject);

            ViewData["name"] = _user.Name;
            ViewData["useremail"] = _user.Email;

            return View(_db.Packages.ToList());
        }

        #endregion

        #region Package Create

        //GET: Packages/Create
        [HttpGet]
        public IActionResult Create()
        {
            var package = new Package();
            return PartialView("Create", package);
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
                    TempData["package"] = "You cannot add " + package.Name + " package because it's type already exist!";
                    TempData["notificationType"] = NotificationType.Error.ToString();
                    return RedirectToAction("Index");
                }

                _db.Packages.Add(package);
                _db.SaveChanges();
                TempData["package"] = "You have successfully added " + package.Name + " as a new package!";
                TempData["notificationType"] = NotificationType.Success.ToString();
                return Json(new { success = true });
            }
            return View(package);
        }

        #endregion

        #region Package Edit

        //GET: Package/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _db.Packages.SingleOrDefaultAsync(p => p.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }
            return PartialView("Edit", package);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Package package, int id)
        {
            if (id != package.PackageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(package);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackageExists(package.PackageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["package"] = "You have successfully modified " + package.Name + " package!";
                TempData["notificationType"] = NotificationType.Success.ToString();
                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }



        #endregion

        #region Package Delete

        //GET: Package/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _db.Packages.SingleOrDefaultAsync(p => p.PackageId == id);

            if (package == null)
            {
                return NotFound();
            }

            return PartialView("Delete", package);
        }

        //POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var package = await _db.Packages.SingleOrDefaultAsync(p => p.PackageId == id);
            if (package != null)
            {
                _db.Packages.Remove(package);
                await _db.SaveChangesAsync();

                TempData["package"] = "You have successfully deleted " + package.Name + " pacakge!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();
                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Package Exists

        private bool PackageExists(int id)
        {
            return _db.Packages.Any(p => p.PackageId == id);
        }

        #endregion



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.RestaurantController
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _db;

        #region Constructor

        public RolesController(ApplicationDbContext context)
        {
            _db = context;
        }

        #endregion

        #region Roles Index

        public async Task <IActionResult> Index()
        {
            var restaurant = HttpContext.Session.GetInt32("RId");
            ViewData["RId"] = HttpContext.Session.GetInt32("RId");
            var roles = _db.Roles.Where(r => r.Name != "Manager" && r.Name != "CEO" && r.RestaurantId == restaurant)
                                        .Include(r => r.Restuarant);

            if (roles != null)
            {
                return View(await roles.ToListAsync());
            }
            else
            {
                return RedirectToAction("Restaurants", "Access");
            }
            
        }

        #endregion

        #region Roles Create

        //GET: Roles/Create
        [HttpGet]
        public IActionResult Create()
        {
            var role = new Role();
            return PartialView("Create", role);
        }
        
        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role)
        {
            var restaurant = HttpContext.Session.GetInt32("RID");

            if (ModelState.IsValid)
            {
                if (restaurant != null)
                {
                    role.RestaurantId = restaurant;
                }

                var roles = _db.Roles;
                foreach (var item in roles)
                {
                    if (item.Name == role.Name)
                    {
                        TempData["message"] = "Role already exist, try another role name!";
                        TempData["notificationtype"] = NotificationType.Error.ToString();
                        return View(role);
                    }
                }

                await _db.Roles.AddAsync(role);
                await _db.SaveChangesAsync();
                TempData["message"] = "You have successfully created a role!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return Json(new { success = true });
            }

            return View(role);
        }
        #endregion

        #region Roles Edit

        //GET: Roles/Edit/4
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = _db.Roles.Find(id);

            if (role != null)
            {
                return NotFound();
            }
            
            return PartialView("Edit", role);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Role role)
        {
            var restaurant = HttpContext.Session.GetInt32("RID");
            if (ModelState.IsValid)
            {
                _db.Entry(role).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                TempData["message"] = "You have successfully modified a role!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return Json(new { success = true });
            }

            return View(role);
        }

        #endregion

        #region Roles Details

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Role role = _db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            return PartialView("Details", role);
        }

        #endregion

        #region Roles Delete

        // GET: Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Role role = _db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            return PartialView("Delete", role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DeleteConfirmed(long id)
        {
            Role role = _db.Roles.Find(id);
            _db.Roles.Remove(role);
            await _db.SaveChangesAsync();
            TempData["message"] = "You have successfully deleted a role!";
            TempData["notificationtype"] = NotificationType.Success.ToString();
            return RedirectToAction("Index");
        }


        #endregion

        #region Roles Exists



        #endregion

    }
}
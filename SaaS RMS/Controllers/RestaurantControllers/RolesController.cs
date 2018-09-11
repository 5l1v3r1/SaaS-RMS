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

namespace SaaS_RMS.Controllers.RestaurantControllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public RolesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Roles Index

        public async Task <IActionResult> Index()
        {
            var restaurant = _session.GetInt32("restaurantsessionid");
            
            var roles = _db.Roles.Where(r => r.Name != "Manager" && r.Name != "CEO" && r.RestaurantId == restaurant);

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
            var restaurant = _session.GetInt32("restaurantsessionid");

            if (ModelState.IsValid)
            {
                if (restaurant != null)
                {
                    role.RestaurantId = Convert.ToInt32(restaurant);
                }

                var roles = _db.Roles;
                foreach (var item in roles)
                {
                    if (item.RestaurantId == restaurant && item.Name == role.Name)
                    {
                        TempData["role"] =  role.Name + " role already exist, try another role name!";
                        TempData["notificationtype"] = NotificationType.Error.ToString();
                        return RedirectToAction("Index");
                    }
                }

                var _role = new Role()
                {
                    Name = role.Name,
                    CreatedBy = _session.GetInt32("loggedinuserid"),
                    LastModifiedBy = Convert.ToInt32(_session.GetInt32("loggedinuserid")),
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    CanManageEmployee = role.CanManageEmployee,
                    CanManageOrders = role.CanManageOrders,
                    CanDoSomething = true
                };

                await _db.Roles.AddAsync(_role);
                await _db.SaveChangesAsync();
                TempData["role"] = "You have successfully created a role!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Roles Edit

        //GET: Roles/Edit/4
        [HttpGet]
        public async Task <IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _db.Roles.SingleOrDefaultAsync(r => r.RoleId == id);

            if (role == null)
            {
                return NotFound();
            }
            
            return PartialView("Edit", role);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var restaurant = _session.GetInt32("restaurantsessionid");
                    if (restaurant != null)
                    {
                        role.RestaurantId = Convert.ToInt32(restaurant);
                    }

                    _db.Entry(role).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.RoleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
                TempData["role"] = "You have successfully modified " + role.Name + " role!";
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
        [HttpGet]
        public async Task <ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Role role = await _db.Roles
                .SingleOrDefaultAsync(r => r.RoleId == id);

            if (role == null)
            {
                return NotFound();
            }
            return PartialView("Delete", role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DeleteConfirmed(int id)
        {
            Role role = await _db.Roles.SingleOrDefaultAsync(r => r.RoleId == id);

            if (role != null)
            {
                _db.Roles.Remove(role);
                await _db.SaveChangesAsync();

                TempData["role"] = "You have successfully deleted " + role.Name + " role!";
                TempData["notificationtype"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }


        #endregion

        #region Roles Exists

        private bool RoleExists(int id)
        {
            return _db.Roles.Any(e => e.RoleId == id);
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.RestaurantController
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public DepartmentsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Department Index

        public async Task<IActionResult> Index()
        {
            var restaurant = _session.GetInt32("RId");

            var department = _db.Departments.Where(d => d.RestaurantId == restaurant)
                             .Include(d => d.Restaurant);

            if (department != null)
            {
                return View(await department.ToListAsync());
            }
            else
            {
                return RedirectToAction("Restaurants", "Access");
            }
        }

        #endregion

        #region Department Create

        //GET: Departments/Create
        [HttpGet]
        public IActionResult Create()
        {
            var department = new Department();
            return PartialView("Create", department);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            var restaurant = _session.GetInt32("RId");

            if (ModelState.IsValid)
            {
                if (restaurant != null)
                {
                    department.RestaurantId = Convert.ToInt32(restaurant);

                    var allDepartments = await _db.Departments.ToListAsync();
                    if (allDepartments.Any(d => d.Name == department.Name && d.RestaurantId == restaurant))
                    {
                        TempData["department"] = "You cannot add this department because it already exist!!!";
                        TempData["notificationType"] = NotificationType.Error.ToString();
                        return RedirectToAction("Index");
                    }

                    //var departments = _db.Departments;
                    //foreach (var item in departments)
                    //{
                    //    if (item.RestaurantId == restaurant && item.Name == department.Name)
                    //    {
                    //        TempData["role"] = "Role already exist, try another role name!";
                    //        TempData["notificationtype"] = NotificationType.Error.ToString();
                    //        return RedirectToAction("Index");
                    //    }
                    //}


                    await _db.AddAsync(department);
                    await _db.SaveChangesAsync();

                    TempData["department"] = "You have successfully added a new Department!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();
                    return Json(new { success = true });
                }

                else
                {
                    TempData["department"] = "Session Expired,Login Again";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Restaurant", "Access");
                }   
            }

            return RedirectToAction("Index");

        }

        #endregion

        #region Department Edit

        //GET: Departments/Edit/4
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _db.Departments.SingleOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
            {
                return NotFound();
            }

            return PartialView("Edit", department);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var restaurant = _session.GetInt32("RId");
                    if (restaurant != null)
                    {
                        department.RestaurantId = Convert.ToInt32(restaurant);
                    }

                    _db.Update(department);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DepartmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["department"] = "You have successfully modified a role!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Department Delete

        //GET: Department/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department department = await _db.Departments
                .Include(d => d.RestaurantId)
                .Include(d => d.Employee)
                .SingleOrDefaultAsync(d => d.DepartmentId == id);

            if (department == null)
            {
                return NotFound();
            }

            return PartialView("Delete", department);
        }

        //POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Department department = await _db.Departments.SingleOrDefaultAsync(d => d.DepartmentId == id);

            if (department != null)
            {
                _db.Departments.Remove(department);
                await _db.SaveChangesAsync();

                TempData["department"] = "You have successfully deleted a department!";
                TempData["notificationtype"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Department Exists

        private bool DepartmentExists(int id)
        {
            return _db.Departments.Any(e => e.DepartmentId == id);
        }

        #endregion

        #region Department Employees

        public async Task<IActionResult> Employees(int? id)
        {
            var restaurant = _session.GetInt32("RId");

            
            //var employeesss = employee.Where(employee.RestaurantId == restaurant);

            //var allEmployeeInDepartment = employee.Departments.Where(employee.RestaurantId == restaurant);


            if (id == null)
            {
                return NotFound();
            }

            Department department = await _db.Departments
                .Include(d => d.RestaurantId)
                .Include(d => d.EmployeeId)
                .SingleOrDefaultAsync(d => d.DepartmentId == id);
            
            if (department == null)
            {
                return NotFound();
            }

            var allEmployeeInDepartment = _db.Employees.Where(r => r.RestaurantId == restaurant).ToListAsync();


            return PartialView("Employees", allEmployeeInDepartment);
        }

        #endregion

    }
}
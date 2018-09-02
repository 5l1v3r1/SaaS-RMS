using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SaaS_RMS.Data;
using SaaS_RMS.Extensions;
using SaaS_RMS.Models.Entities.Employee;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Enums;
using SaaS_RMS.Services;

namespace SaaS_RMS.Controllers.EmployeeControllers
{
    public class EmployeeManagementController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public EmployeeManagementController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
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

        #region Add Employee

        //GET: EmployeeManagement/AddEmployee
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        //POST: EmployeeManagement/AddEmployee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee(PreEmployee preEmployee)
        {
            //var userId = _session.GetInt32("loggedinusersessionid");
            var restaurantId = _session.GetInt32("restaurantsessionid");
            var restaurant = _db.Restaurants.Find(restaurantId);

            try
            {
                if (_db.EmployeePersonalDatas.Any(n => n.Email == preEmployee.Email) == false && 
                    _db.AppUsers.Any(n => n.Email == preEmployee.Email) == false)
                {
                    var _employee = new Employee
                    {
                        RestaurantId = Convert.ToInt32(restaurantId),
                        //CreatedBy = userId,
                        //LastModifiedBy = userId,
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now
                    };

                    _db.Employees.Add(_employee);
                    _db.SaveChangesAsync();

                    if (_employee.EmployeeId > 0)
                    {
                        //Popluate the personal data object
                        var employeePersonalData = new EmployeePersonalData
                        {
                            
                        };
                    }
                }
            }
            catch
            {

            }
            return View();
        }

        #endregion

        #region Employee Personal Data
        

        #endregion

        #region Employee Educational Qualification
        

        #endregion

        #region Employee Past Work Experience
        



        #endregion

        #region Employee Family Data
        


        #endregion

        #region Employee Medical Data



        #endregion

        #region List Of Employee Data

        //GET: 

        #endregion

    }
}
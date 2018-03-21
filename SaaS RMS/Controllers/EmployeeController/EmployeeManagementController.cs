using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Employee;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.EmployeeController
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

        #region Employee Personal Data

        //GET: EmployeeManagement/PersonalData
        [HttpGet]
        public async Task<IActionResult> PersonalData(bool? returnUrl, bool? backUrl)
        {
            var restaurant = _session.GetInt32("RId");
            var _employee = _db.Employees.Find(restaurant);
            

            ViewBag.State = new SelectList(_db.States, "StateId", "Name");

            if (returnUrl != null && returnUrl.Value)
            {
                ViewBag.returnUrl = true;
                if (_employee != null)
                {
                    return View(_employee.EmployeePersonalDatas.SingleOrDefault());
                }
            }

            if (backUrl != null && backUrl.Value)
            {
                if (_employee != null)
                {
                    return View(_employee.EmployeePersonalDatas.SingleOrDefault());
                }
            }
            
            var statistics = new RestaurantStatistics();
            if (restaurant != null)
            {
                statistics.RestaurantId = restaurant;
            }

            statistics.Action = StatisticsEnum.Registration.ToString();
            statistics.DateOccured = DateTime.Now;

            await _db.AddAsync(statistics);
            await _db.SaveChangesAsync();

            return View();
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PersonalData(EmployeePersonalData personalData, FormCollection collectedValues)
        {
            var restaurant = _session.GetInt32("RId");

            var allEmployees = _db.EmployeePersonalDatas;

            var _employee = _db.Employees.Find(restaurant);

            if (_employee != null)
            {
                _employee.EmployeePersonalDatas = new List<EmployeePersonalData> { personalData };
            }

            if (allEmployees.Any(p => p.Email == personalData.Email))
            {
                TempData["personal"] = "The email already exists!";
                TempData["notificationType"] = NotificationType.Error.ToString();
                //return next view
                ViewBag.State = new SelectList(_db.States, "StateId", "Name", personalData.StateId);
                return View(personalData);
            }

            var returnUrl = Convert.ToBoolean(collectedValues["returnUrl"]);
            //if it is edit from review page return to the review page
            if (returnUrl)
            {
                return View("ReviewEmployeeData");
            }
            //return next view
            return RedirectToAction("EducationalQualification");
        }

        #endregion

        #region Employee Medical Data



        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Employee;

namespace SaaS_RMS.Controllers.EmployeeController
{
    public class EmployeeMangementController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public EmployeeMangementController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Fetch Data

        #endregion

        #region Employee Process

        //GET: EmployeeManagement/PersonalData
        [HttpGet]
        public IActionResult PersonalData(bool? returnUrl, bool? backUrl)
        {

            var restaurant = _session.GetInt32("RID");
            var _employee = _db.Employees.Find(restaurant);

            ViewData["State"] = new SelectList(_db.States, "StateId", "Name");

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
            return View();
            
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}
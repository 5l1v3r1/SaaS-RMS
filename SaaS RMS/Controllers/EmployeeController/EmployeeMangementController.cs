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

        #region Constructor

        public EmployeeMangementController(ApplicationDbContext context)
        {
            _db = context;
        }

        #endregion

        #region Fetch Data

        #endregion

        #region Employee Process

        //GET: EmployeeManagement/PersonalData
        [HttpGet]
        public IActionResult PersonalData(bool? returnUrl, bool? backUrl)
        {

            //var restaurant = HttpContext.Session.GetInt32("RID");
            //HttpContext.Session.SetString("Employee", employee)

            //ViewData["State"] = new SelectList(_db.States, "StateId", "Name");

            //if (returnUrl != null && returnUrl.Value)
            //{
            //    ViewBag.returnUrl = true;
            //    if (_employee != null)
            //    {
            //        return View(_employee.E)
            //    }
            //}
            return View();
            
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }
    }
}
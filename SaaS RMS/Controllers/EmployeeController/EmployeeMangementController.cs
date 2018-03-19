using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SaaS_RMS.Controllers.EmployeeController
{
    public class EmployeeMangementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
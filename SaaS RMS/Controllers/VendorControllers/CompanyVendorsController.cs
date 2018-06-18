using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Vendor;

namespace SaaS_RMS.Controllers.VendorControllers
{
    public class CompanyVendorsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public CompanyVendorsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Register

        //GET: CompanyVendor/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //POST: 
        [HttpPost]
        public async Task<IActionResult> Register(CompanyVendor companyVendor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_db.CompanyVendors.Any(cv => cv.Name == companyVendor.Name))
                    {
                        ViewData["doublename"] = "Vendor name already exists";
                    }
                    else
                    {
                        await _db.CompanyVendors.AddAsync(companyVendor);
                        await _db.SaveChangesAsync();
                        return RedirectToAction("Restaurant", "Access");
                    }

                    return View();
                }
            }
            catch
            {
                return View();
            }

            return View();
        }
        #endregion
    }
}
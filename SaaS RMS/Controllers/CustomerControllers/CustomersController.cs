using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Customer;

namespace SaaS_RMS.Controllers.CustomerControllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public CustomersController (ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Register

        //GET: Customers/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(_db.Customer.Any( c => c.Email  == customer.Email))
                    {
                        ViewData["doubleemail"] = "Sorry Email already exists";
                    }
                    else
                    {
                        await _db.Customer.AddAsync(customer);
                        await _db.SaveChangesAsync();

                        return RedirectToAction("SignIn", "Customers");
                    }
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
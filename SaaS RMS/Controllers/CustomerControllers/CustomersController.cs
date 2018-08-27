using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Customer;
using SaaS_RMS.Services;

namespace SaaS_RMS.Controllers.CustomerControllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IEmailSender _emailSender;

        #region Constructor

        public CustomersController (ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IEmailSender emailSender)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
        }

        #endregion

        #region Register

        //GET: Customers/Register
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Customer customer, string returnUrl = null)
        {
            try
            {
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    if(_db.Customer.Any( c => c.Email  == customer.Email))
                    {
                        ViewData["doubleemail"] = "Sorry Email already exists";
                    }
                    else
                    {
                        
                        customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);
                        customer.ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(customer.ConfirmPassword);

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

        #region SignIn

        //GET: Customers/Signin
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(Customer customer)
        {
            try
            {
                var _customer = _db.Customer.Where(c => c.Email == customer.Email).SingleOrDefault();

                if (_customer != null)
                {
                    var _password = BCrypt.Net.BCrypt.Verify(customer.Password, _customer.Password);

                    if (_password == true)
                    {
                        _session.SetInt32("customersessionid", _customer.CustomerId);
                        _session.SetString("customerfirstname", _customer.FirstName);
                        _session.SetString("customerlastname", _customer.LastName);
                        _session.SetString("customeremailaddress", _customer.Email);
                        _session.SetString("customerphonenumber", _customer.PhoneNumber);

                        ViewData["checker"] = _session.GetInt32("customersessionid");

                        return RedirectToAction("Landing", "Home");
                    }
                    else
                    {
                        ViewData["mismatch"] = "Email and Password do not match";
                    }
                }
                else
                {
                    ViewData["mismatch"] = "Email doesn't exist";
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region Dashboard

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var customerid = _session.GetInt32("customersessionid");

            if(customerid == null)
            {
                return RedirectToAction("Home", "Landing");
            }

            var _customer = await _db.Customer.FindAsync(customerid);

            ViewData["customerfirstname"] = _customer.FirstName;
            ViewData["customerlastname"] =  _customer.LastName;

            return View();
        }

        #endregion
        
        #region MyRegion

        #endregion


    }
}
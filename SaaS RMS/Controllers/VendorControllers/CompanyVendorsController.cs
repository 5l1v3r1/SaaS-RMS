using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Vendor;
using SaaS_RMS.Models.Enums;

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
        [Route("Vendor/Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //POST: 
        [HttpPost]
        [Route("Vendor/Register")]
        [ValidateAntiForgeryToken]
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
                        var _companyVendor = new CompanyVendor
                        {
                            Name = companyVendor.Name,
                            Address = companyVendor.Address,
                            ContactNumber = companyVendor.ContactNumber,
                            OfficeNumber = companyVendor.OfficeNumber,
                            VendorType = Models.Enums.VendorType.Registered,
                            Password = BCrypt.Net.BCrypt.HashPassword(companyVendor.Password),
                            ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(companyVendor.ConfirmPassword),
                            VendorItem = companyVendor.VendorItem,
                        };

                        await _db.CompanyVendors.AddAsync(_companyVendor);
                        await _db.SaveChangesAsync();
                        ModelState.Clear();
                        return RedirectToAction("SignIn", "CompanyVendors");
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

        #region SignIn

        //GET: CompanyVendor/SignIn
        [Route("Vendor/SignIn")]
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        //POST:
        [Route("Vendor/SignIn")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult SignIn(CompanyVendor companyVendor)
        {
            try
            {
                var _companyVendor = _db.CompanyVendors.Where(cv => cv.Name == companyVendor.Name).SingleOrDefault();

                var _password = BCrypt.Net.BCrypt.Verify(companyVendor.Password, _companyVendor.Password);
                if (_password == true)
                {
                    _session.SetInt32("companyvendorid", _companyVendor.CompanyVendorId);
                    _session.SetString("companyvendorname", _companyVendor.Name);
                    _session.SetString("companyvendorpassword", _companyVendor.Password);
                    _session.SetString("companyvendorcomfirmpassword", _companyVendor.ConfirmPassword);
                    _session.SetString("companyvendoraddress", _companyVendor.Address);
                    _session.SetString("companyvendorcontactnumber", _companyVendor.ContactNumber);
                    _session.SetString("companyvendorofficenumber", _companyVendor.OfficeNumber);
                    _session.SetString("companyvendorvendoritem", _companyVendor.VendorItem);
                    
                    ModelState.Clear();
                    return RedirectToAction("Dashboard", "CompanyVendors");
                }
                else
                {
                    ViewData["mismatch"] = "The Vendor Name and Password do not match";
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

        [Route("Vendor/Dashboard")]
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var companyvendorid = _session.GetInt32("companyvendorid");

            if (companyvendorid == null)
            {
                return RedirectToAction("signIn", "CompanyVendors");
            }

            var _companyVendor = await _db.CompanyVendors.FindAsync(companyvendorid);
            ViewData["companyvendorname"] = _companyVendor.Name;


            return View();
        }

        #endregion

        #region Profile

        [HttpGet]
        [Route("Vendor/Profile")]
        public async Task<IActionResult> Profile()
        {
            var companyvendorid = _session.GetInt32("companyvendorid");

            if(companyvendorid == null)
            {
                return RedirectToAction("SignIn", "CompanyVendors");
            }

            var _companyVendor = await _db.CompanyVendors.FindAsync(companyvendorid);

            ViewData["companyvendorname"] = _companyVendor.Name;
            
            var profile = await _db.CompanyVendors.SingleOrDefaultAsync(cv => cv.CompanyVendorId == companyvendorid);

            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Vendor/Profile")]
        public async Task<IActionResult> Profile(CompanyVendor companyVendor)
        {
            var companyvendorid = _session.GetInt32("companyvendorid");

            if (companyvendorid == null)
            {
                return RedirectToAction("Signin", "CompanyVendors");
            }

            if (companyvendorid != companyVendor.CompanyVendorId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    companyVendor.Password = _session.GetString("companyvendorpassword");
                    companyVendor.ConfirmPassword = _session.GetString("companyvendorcomfirmpassword");

                    companyVendor.VendorType = VendorType.Registered;
                        
                    _db.Update(companyVendor);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyVendorExists(companyVendor.CompanyVendorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["companyvendor"] = "Profile Updated!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return View("Dashboard");
            }

            return RedirectToAction("Dashboard");

        }
        #endregion

        #region Change Password

        [HttpGet]
        [Route("Vendor/ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            var companyvendorid = _session.GetInt32("companyvendorid");

            if (companyvendorid == null)
            {
                return RedirectToAction("SignIn", "CompanyVendors");
            }

            var _companyVendor = await _db.CompanyVendors.FindAsync(companyvendorid);
            
            var changePassword = await _db.CompanyVendors.SingleOrDefaultAsync(cv => cv.CompanyVendorId == companyvendorid);

            if (changePassword == null)
            {
                return NotFound();
            }

            return View(changePassword);
        }

        [HttpPost]
        [Route("Vendor/ChangePassword")]
        public async Task<IActionResult> ChangePassword(CompanyVendor companyVendor)
        {
            var companyvendorid = _session.GetInt32("companyvendorid");

            if (companyvendorid == null)
            {
                return RedirectToAction("Signin", "CompanyVendors");
            }

            if (companyvendorid != companyVendor.CompanyVendorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    companyVendor.Password = BCrypt.Net.BCrypt.HashPassword(companyVendor.Password);
                    companyVendor.ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(companyVendor.ConfirmPassword);
                    companyVendor.Name = _session.GetString("companyvendorname");
                    companyVendor.OfficeNumber = _session.GetString("companyvendorofficenumber");
                    companyVendor.Address = _session.GetString("companyvendoraddress");
                    companyVendor.ContactNumber = _session.GetString("companyvendorcontactnumber");
                    companyVendor.VendorItem = _session.GetString("companyvendorvendoritem");
                    companyVendor.VendorType = VendorType.Registered;

                    _db.Update(companyVendor);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyVendorExists(companyVendor.CompanyVendorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["companyvendor"] = "   Password changed!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return View("Dashboard");
            }

            return RedirectToAction("Dashboard");
        }

        #endregion
        
        #region Sign Out

        [Route("Vendor/SignOut")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignOut()
        {
            _session.Remove("companyvendorid");
            _session.Remove("compnayvendorname");
            _session.Remove("companyvendorofficenumber");
            _session.Remove("companyvendorpassword");
            _session.Remove("companyvendorconfirmpassword");
            _session.Remove("compnayvendoraddress");
            _session.Remove("companyvendorcontactnumber");
            _session.Remove("companyvendorvendoritem");

            return RedirectToAction("SignIn", "CompanyVendors");
        }

        #endregion
        
        #region CompanyVendor Exists

        private bool CompanyVendorExists(int id)
        {
            return _db.CompanyVendors.Any(b => b.CompanyVendorId == id);
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SaaS_RMS.Data;
using SaaS_RMS.Extensions;
using SaaS_RMS.Models.Encryption;
using SaaS_RMS.Models.Entities.Employee;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Entities.System;
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

        #region Employee
            
        [HttpGet]
        [Route("Employee/List")]
        public IActionResult Employee()
        {
            var transport = new ApplicationTransport
            {
                EmployeeWorkDatas = _db.EmployeeWorkDatas.Include(ewd => ewd.Department).ToList(),
                Employees = _db.Employees.ToList(),
                EmployeePersonalDatas = _db.EmployeePersonalDatas.ToList()
            };

            return View(transport);
        }

        #endregion

        #region Add Employee

        //GET: EmployeeManagement/AddEmployee
        [Route("Employee/Add")]
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        //POST: EmployeeManagement/AddEmployee
        [Route("Employee/Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> AddEmployee(PreEmployee preEmployee)
        {
            var userId = _session.GetInt32("loggedinusersessionid");
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
                        CreatedBy = userId,
                        LastModifiedBy = Convert.ToInt32(userId),
                        DateCreated = DateTime.Now,
                        DateLastModified = DateTime.Now
                    };

                    _db.Employees.Add(_employee);
                    await _db.SaveChangesAsync();

                    if (_employee.EmployeeId > 0)
                    {
                        //Popluate the personal data object
                        var _employeePersonalData = new EmployeePersonalData
                        {
                            RestaurantId = Convert.ToInt32(restaurantId),
                            CreatedBy = userId,
                            LastModifiedBy = Convert.ToInt32(userId),
                            DateCreated = DateTime.Now,
                            DateLastModified = DateTime.Now,
                            FirstName = preEmployee.Firstname,
                            LastName = preEmployee.Lastname,
                            Email = preEmployee.Email,
                            PrimaryAddress = preEmployee.PrimaryAddress,
                            SecondaryAddress = "N/A",
                            State = "N/A",
                            MiddleName = "N/A",
                            LGA = "N/A",
                            HomePhone = preEmployee.HomePhoneNumber,
                            WorkPhone = "N/A",
                            DOB = DateTime.Now,
                            Title = 0.ToString(),
                            MaritalStatus = 0.ToString(),
                            Gender = 0.ToString(),
                            POB = "N/A",
                            EmployeeId = _employee.EmployeeId
                        };

                        _db.EmployeePersonalDatas.Add(_employeePersonalData);
                        await _db.SaveChangesAsync();

                        var password = new Md5Encryption().RandomString(7);
                        var _appUser = new AppUser
                        {
                            EmployeeId = _employee.EmployeeId,
                            Email = _employeePersonalData.Email,
                            Name = _employeePersonalData.DisplayName,
                            RestaurantId = Convert.ToInt32(restaurantId),
                            CreatedBy = userId,
                            LastModifiedBy = Convert.ToInt32(userId),
                            DateCreated = DateTime.Now,
                            DateLastModified = DateTime.Now,
                            Password = new Hashing().HashPassword(password),
                            ConfirmPassword = new Hashing().HashPassword(password),
                            Status = UserStatus.Inactive.ToString()
                        };

                        _db.AppUsers.Add(_appUser);
                        await _db.SaveChangesAsync();

                        if(_appUser.AppUserId > 0)
                        {
                            //define acceskeys and save transactions
                            var accesskey = new AppUserAccessKey
                            {
                                PasswordAccessCode = new Md5Encryption().RandomString(15),
                                AccountActivationAccessCode = new Md5Encryption().RandomString(20),
                                CreatedBy = _appUser.AppUserId,
                                LastModifiedBy = _appUser.AppUserId,
                                DateCreated = DateTime.Now,
                                DateLastModified = DateTime.Now,
                                ExpiryDate = DateTime.Now.AddDays(1),
                                AppUserId = _appUser.AppUserId 
                            };

                            _db.AppUserAccessKeys.Add(accesskey);
                            await _db.SaveChangesAsync();
                            //new Mailer()
                        }

                        TempData["display"] = "You have successfully added a new employee!";
                        TempData["notificationType"] = NotificationType.Success.ToString();
                        return View();
                    }

                    TempData["display"] = "There is an error performing this action. Try again!";
                    TempData["notificationType"] = NotificationType.Error.ToString();
                    return View(preEmployee);
                }

                TempData["display"] = "The employee already exist, try a different email!";
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return View(preEmployee);
            }
            catch(Exception ex)
            {
                TempData["display"] = ex.Message;
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return View();
            }
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
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Newtonsoft.Json;
//using SaaS_RMS.Data;
//using SaaS_RMS.Extensions;
//using SaaS_RMS.Models.Entities.Employee;
//using SaaS_RMS.Models.Entities.Restuarant;
//using SaaS_RMS.Models.Enums;
//using SaaS_RMS.Services;

//namespace SaaS_RMS.Controllers.EmployeeControllers
//{
//    public class EmployeeManagementController : Controller
//    {
//        private readonly ApplicationDbContext _db;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private ISession _session => _httpContextAccessor.HttpContext.Session;

//        #region Constructor

//        public EmployeeManagementController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
//        {
//            _db = context;
//            _httpContextAccessor = httpContextAccessor;
//        }

//        #endregion

//        #region Fetch Data

//        public JsonResult GetLgasForState(int id)
//        {
//            var lgas = _db.Lgas.Where(l => l.StateId == id);
//            return Json(lgas);
//        }

//        #endregion

//        #region Add Employee

//        [HttpGet]
//        public IActionResult AddEmployee()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult AddEmployee(PreEmployee preEmployee)
//        {
//            return View();
//        }

//        #endregion

//        #region Employee Personal Data

//        //GET: EmployeeManagement/PersonalData
//        [HttpGet]
//        public async Task<IActionResult> PersonalData(bool? returnUrl, bool? backUrl)
//        {
//            var restaurant = _session.GetInt32("restaurantsessionid");
            
//            var _employee = _db.Employees.Find(restaurant);

//            ViewBag.State = new SelectList(_db.States, "StateId", "Name");

//            if (returnUrl != null && returnUrl.Value)
//            {
//                ViewBag.returnUrl = true;
//                if (_employee != null)
//                {
//                    return View(_employee.EmployeePersonalDatas.SingleOrDefault());
//                }
//            }

//            if (backUrl != null && backUrl.Value)
//            {
//                if (_employee != null)
//                {
//                    return View(_employee.EmployeePersonalDatas.SingleOrDefault());
//                }
//            }
            
//            var statistics = new RestaurantStatistics();
//            if (restaurant != null)
//            {
//                statistics.RestaurantId = restaurant;
//                statistics.Action = StatisticsEnum.Registration.ToString();
//                statistics.DateOccured = DateTime.Now;

//                await _db.AddAsync(statistics);
//                await _db.SaveChangesAsync();
//            }

            

//            return View();
//        }

//        //POST:
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult PersonalData(EmployeePersonalData personalData, FormCollection collectedValues)
//        {
//            var employee = new Employee();

//            var restaurant = _session.GetInt32("restaurantsessionid");
            
//            var allEmployees = _db.EmployeePersonalDatas;

//            var _employee = _db.Employees.Find(restaurant);
            

//            if (_employee != null)
//            {
//                personalData.Title = typeof(NameTitle).GetEnumName(int.Parse(personalData.Title));
//                personalData.DOB = Convert.ToString(Convert.ToDateTime(collectedValues["DOB"]));

//                _employee.EmployeePersonalDatas = new List<EmployeePersonalData> { personalData };
//                //_session.("Employee")
//                _session.SetObject("Employee", _employee);
                
//            }
//            else
//            {
//                var employeePersonalData = new Employee();
//                personalData.DOB = Convert.ToString(Convert.ToDateTime(collectedValues["DOB"]));
//                personalData.Title = typeof(NameTitle).GetEnumName(int.Parse(personalData.Title));
//                _session.SetObject("Employee", employeePersonalData);

//            }
//            if (allEmployees.Any(p => p.Email == personalData.Email))
//            {
//                TempData["personal"] = "The email already exists!";
//                TempData["notificationType"] = NotificationType.Error.ToString();
//                //return next view
//                ViewBag.State = new SelectList(_db.States, "StateId", "Name", personalData.StateId);
//                return View(personalData);
//            }

//            var returnUrl = Convert.ToBoolean(collectedValues["returnUrl"]);
//            //if it is edit from review page return to the review page
//            if (returnUrl)
//            {
//                return View("ReviewEmployeeData");
//            }
//            //return next view
//            return RedirectToAction("EducationalQualification");
//        }

//        #endregion

//        #region Employee Educational Qualification

//        //GET: EmploymentManagement/EducationalQualification
//        [HttpGet]
//        public IActionResult EducationalQualification(bool? returnUrl)
//        {
//            var restaurant = _session.GetInt32("restaurantsessionid");

//            var _employee = _db.Employees.Find(restaurant);
            
//            ViewBag.RestaurantQualificationId = new SelectList(_db.RestaurantQualifications.Where(rq => rq.RestaurantId == restaurant));

//            if (returnUrl != null && returnUrl.Value)
//            {
//                ViewBag.returnUrl = true;
//                if (_employee != null)
//                {
//                    return View();
//                }
//            }
//            return View();
//        }


//        //POST: EmploymentManagement/EducationalQualification
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult EducationalQualification(FormCollection collectedValues)
//        {
//            var restaurant = _session.GetInt32("restaurantsessionid");
//            var _employee = _db.Employees.Find(restaurant);

//            //collect data from form using form collection
//            var returnUrl = Convert.ToBoolean(collectedValues["returnUrl"]);

//            var file = Request.Form.Files["file"];

//            if (_employee != null)
//            {
//                string degree = null;
//                string classOfDegree = null;
//                int? qualification = null;

//                if (collectedValues["DegreeAttained"] != "")
//                {
//                    degree = typeof(DegreeTypeEnum).GetEnumName(int.Parse(collectedValues["DegreeAttained"]));
//                }

//                if (collectedValues["ClassOfDegree"] != "")
//                {
//                    classOfDegree = typeof(ClassOfDegree).GetEnumName(int.Parse(collectedValues["ClassOfDegree"]));
//                }

//                if (collectedValues["RestaurantQualificationId"] != "")
//                {
//                    qualification = Convert.ToInt32(collectedValues["RestaurantQualificationId"]);
//                }

//                if (_employee.EmployeeEducationalQualifications == null)
//                {
//                    _employee.EmployeeEducationalQualifications = new List<EmployeeEducationalQualification>();
//                }

//                _employee.EmployeeEducationalQualifications.Add(new EmployeeEducationalQualification
//                {
//                    ClassOfDegree = classOfDegree,
//                    DegreeAttained = degree,
//                    Name = collectedValues["Name"],
//                    Location = collectedValues["Location"],
//                    StartDate = Convert.ToDateTime(collectedValues["StartDate"]),
//                    EndDate = Convert.ToDateTime(collectedValues["EndDate"]),
//                    RestaurantQualificationId = qualification,
//                    FileUpload = 
//                        file != null && file.FileName != ""
//                        ? new FileUploader().UploadFile(file, UploadType.Education)
//                        : null
//                });
//                TempData["education"] = "You ave successfully added a " + degree + " qualification!";
//                TempData["notificationType"] = NotificationType.Success.ToString();

//                _session.SetObject("Employee", _employee);

//            }
//            //if it is edit from review page return to the review page
//            if (returnUrl)
//                return RedirectToAction("EducationalQualification", new { returnUrl = true });
//            return RedirectToAction("EducationalQualification");
//        }

//        #endregion

//        #region Employee Past Work Experience

//        //GET: EmployeeManagement/PastWorkExperience
//        [HttpGet]
//        public IActionResult PastWorkExperience(bool? returnUrl)
//        {
//            var restaurant = _session.GetInt32("restaurantsessionid");
//            var _employee = _db.Employees.Find(restaurant);

//            if (returnUrl != null && returnUrl.Value)
//            {
//                ViewBag.returnUrl = true;
//                if (_employee != null)
//                {
//                    return View();
//                }
//            }
//            return View();
//        }

//        //POST:
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult PastWorkExperience(FormCollection collectedValues)
//        {
//            var restaurant = _session.GetInt32("restaurantsessionid");
//            var _employee = _db.Employees.Find(restaurant);

//            if (_employee != null)
//            {
//                if (_employee.EmployeePastWorkExperiences == null)
//                {
//                    _employee.EmployeePastWorkExperiences = new List<EmployeePastWorkExperience>();
//                }
//                _employee.EmployeePastWorkExperiences.Add(new EmployeePastWorkExperience
//                {
//                    EmployerName = collectedValues["EmployerName"],
//                    EmployerLocation = collectedValues["EmployerLocation"],
//                    EmployerContact = collectedValues["EmployerContact"],
//                    PositionHeld = collectedValues["PositionHeld"],
//                    ReasonForLeaving = collectedValues["ReasonForLeaving"],
//                    StartDate = Convert.ToDateTime(collectedValues["StartDate"]),
//                    EndDate = Convert.ToDateTime(collectedValues["EndDate"]),
//                    //FakeId = _employee.EmployeePastWorkExperiences.Count + 1
//                });

//                TempData["pastworkexperience"] = "You have successfully added a work experience!";
//                TempData["notificationType"] = NotificationType.Success.ToString();
//            }

//            var returnUrl = Convert.ToBoolean(collectedValues["returlUrl"]);

//            if (returnUrl)
//            {
//                return RedirectToAction("PastWorkExperience", new { returnUrl = true });
//            }
//            return View("PastWorkExperience");
//        }



//        #endregion

//        #region Employee Family Data

//        //GET: EmployeeManagement/FamilyData
//        [HttpGet]
//        public IActionResult FamilyData(bool? returnUrl, bool? backUrl)
//        {
//            var restaurant = _session.GetInt32("restaurantsessionid");
//            var _employee = _db.Employees.Find(restaurant);

//            if (returnUrl != null && returnUrl.Value)
//            {
//                ViewBag.returnUrl = true;
//                if (_employee != null)
//                {
//                    return View(_employee.EmployeeFamilyDatas.SingleOrDefault());
//                }
//            }

//            if(backUrl != null && backUrl.Value)
//            {
//                if (_employee != null)
//                {
//                    return View(_employee.EmployeeFamilyDatas.SingleOrDefault());
//                }
//            }

//            return View();
//        }

//        //POST:
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult FamilyData(EmployeeFamilyData familyData, FormCollection collectedValues)
//        {
//            var restaurant = _session.GetInt32("restaurantsessionid");
//            var _employee = _db.Employees.Find(restaurant);

//            if (_employee != null)
//            {
//                familyData.NextOfKin = collectedValues["FullName"];
//                familyData.ContactNumber = collectedValues["ContactNumber"];
//                familyData.Address = collectedValues["Address"];
//                familyData.Email = collectedValues["Email"];
//                familyData.Relationship = typeof(FamilyEnum).GetEnumName(int.Parse(collectedValues["Relationship"]));
//                familyData.DOB = Convert.ToDateTime(collectedValues["DOB"]);

//                _employee.EmployeeFamilyDatas = new List<EmployeeFamilyData> { familyData };
//            }
//            else
//            {
//                var employeeFamilyData = new Employee();
//                familyData.NextOfKin = collectedValues["FullName"];
//                familyData.ContactNumber = collectedValues["ContactNumber"];
//                familyData.Address = collectedValues["Address"];
//                familyData.Email = collectedValues["Email"];
//                familyData.Relationship = typeof(FamilyEnum).GetEnumName(int.Parse(collectedValues["Relationship"]));
//                familyData.DOB = Convert.ToDateTime(collectedValues["DOB"]);

//                employeeFamilyData.EmployeeFamilyDatas = new List<EmployeeFamilyData> { familyData };
//            }

//            var returnUrl = Convert.ToBoolean(collectedValues["returnUrl"]);
//            //if it is edit from review page return to the review page
//            if (returnUrl)
//            {
//                return View("ReviewEmployeeData");
//            }
//            //return next view
//            return RedirectToAction("BankData");
//        }


//        #endregion

//        #region Employee Medical Data



//        #endregion


//        #region List Of Employee Data

//        //GET: 

//        #endregion





//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}
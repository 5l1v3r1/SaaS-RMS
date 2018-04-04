using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Restuarant;
using SaaS_RMS.Models.Enums;
using SaaS_RMS.Services;

namespace SaaS_RMS.Controllers.RestaurantController
{
    public class MealsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        private readonly IHostingEnvironment _environment;

        #region Constructor

        public MealsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IHostingEnvironment environment)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
            _environment = environment;
        }

        #endregion

        #region Meal Index

        public async Task<IActionResult> Index()
        {
            var restaurant = _session.GetInt32("RId");

            if(restaurant == null)
            {
                return RedirectToAction("Access", "Restaurants");
            }

            var meals = _db.Meals.Where(m => m.RestaurantId == restaurant)
                .Include(m => m.Restaurant)
                .ToListAsync();

            if (meals != null)
            {
                return View(await meals);
            }
            else
            {
                return RedirectToAction("Access", "Restaurants");
            }
        }

        #endregion

        #region Meal Create

        //GET: Meals/Create
        [HttpGet]
        public IActionResult Create()
        {
            //var meal = new Meal();
            return View("Create");
            //return View("Create", meal);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Meal meal, IFormFile file, UploadType uploadType)
        {
            var restaurant = _session.GetInt32("RId");

            //var files = Request.Form.Files["Image"];

            var fileUploader = new FileUploader();
            var filename = DateTime.Now.ToFileTime().ToString();


            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("img_null", "Image is not selected");
            }
            else
            {
                var fileinfo = new FileInfo(file.FileName);

                if ((fileinfo.Extension.ToLower() == ".jpg") || (fileinfo.Extension.ToLower() == ".jpeg") ||
                    (fileinfo.Extension.ToLower() == ".png") || (fileinfo.Extension.ToLower() == ".gif") ||
                    (fileinfo.Extension.ToLower() == ".pdf") || (fileinfo.Extension.ToLower() == ".docx") ||
                    (fileinfo.Extension.ToLower() == ".doc"))
                {
                    try
                    {
                        filename = DateTime.Now.ToFileTime() + fileinfo.Extension;
                        var uploads = Path.Combine(_environment.WebRootPath, "uploads" + uploadType);

                        //check to see if the directory exists else, create directory
                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        if (file.Length > 0)
                        {
                            using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }


                

                if (ModelState.IsValid)
                {
                    if (restaurant != null)
                    {
                        meal.RestaurantId = Convert.ToInt32(restaurant);
                    }

                    meal.Image = filename;
                    await _db.Meals.AddAsync(meal);
                    await _db.SaveChangesAsync();
                    return View("Index");
                    //return Json(new { success = true });
                }
            }

            return View("Index");
        }

        #endregion

        #region Meal View Picture

        public IActionResult View()
        {
            return View(); 
        }

        #endregion
    }
}
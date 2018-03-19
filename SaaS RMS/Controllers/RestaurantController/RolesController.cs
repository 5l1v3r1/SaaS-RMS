using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Restuarant;

namespace SaaS_RMS.Controllers.RestaurantController
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _db;

        #region Constructor

        public RolesController(ApplicationDbContext context)
        {
            _db = context;
        }

        #endregion

        #region Roles Index

        public async Task <IActionResult> Index()
        {
            var restaurant = HttpContext.Session.GetInt32("RId");
            var roles = _db.Roles.Where(r => r.Name != "Manager" && r.Name != "CEO" && r.RestaurantId == restaurant)
                                        .Include(r => r.Restuarant);
            return View(await roles.ToListAsync());
        }

        #endregion

        #region Roles Create

        //GET: Roles/Create
        [HttpGet]
        public IActionResult Create()
        {
            var role = new Role();
            return PartialView("Create", role);
        }
        
        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Role role)
        //{

        //}
        #endregion

        #region Roles Edit



        #endregion

        #region Roles Details

        // GET: Roles/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                //
            }
            Role role = _db.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            return PartialView("Details", role);
        }

        #endregion

        #region Roles Delete



        #endregion

        #region Roles Exists



        #endregion

    }
}
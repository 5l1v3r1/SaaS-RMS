using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;

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
            return View(await _db.Roles.ToListAsync());
        }

        #endregion

        #region Roles Create



        #endregion

        #region Roles Edit



        #endregion

        #region Roles Details



        #endregion

        #region Roles Delete



        #endregion

        #region Roles Exists



        #endregion

    }
}
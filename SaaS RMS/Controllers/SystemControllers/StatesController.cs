using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.System;

namespace SaaS_RMS.Controllers.SystemControllers
{
    public class StatesController : Controller
    {
        private readonly ApplicationDbContext _db;

        #region Constructor

        public StatesController(ApplicationDbContext context)
        {
            _db = context;
        }

        #endregion

        #region State Index

        public async Task<IActionResult> Index()
        {
            return View(await _db.States.ToListAsync());
        }

        #endregion

        #region State Create

        //GET: States/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: States/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(State state)
        {
            if (ModelState.IsValid)
            {
                _db.States.Add(state);
                await _db.SaveChangesAsync();
                return View();
            }
            return View(state);
        }

        #endregion

    }
}
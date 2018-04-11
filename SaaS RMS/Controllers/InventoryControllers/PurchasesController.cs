using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;

namespace SaaS_RMS.Controllers.InventoryControllers
{
    public class PurchasesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public PurchasesController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            if (restaurant == null)
            {
                return RedirectToAction("Access", "Restaurants");
            }

            var purchase = await _db.Purchases.Include(p => p.StockDetail).Include(p => p.PurchaseEntry).ToListAsync();

            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        #endregion



        #region Purchase Exists

        private bool PurchaseExists(int? id)
        {
            return _db.Purchases.Any(p => p.PurchaseEntryId == id);
        }

        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Employee;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.EmployeeController
{
    public class BanksController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public BanksController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Bank Index

        public async Task <IActionResult> Index()
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            if (restaurant == null)
            {
                return RedirectToAction("Access", "Restaurants");
            }

            var bank = _db.Banks.Where(b => b.RestaurantId == restaurant)
                .Include(b => b.RestaurantId)
                .ToListAsync();

            if (bank != null)
            {
                return View(await bank);
            }
            else
            {
                return RedirectToAction("Access", "Restaurants");
            }
        }

        #endregion

        #region Bank Create

        //GET: Banks/Create
        [HttpGet]
        public IActionResult Create()
        {
            var bank = new Bank();
            return PartialView("Create", bank);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bank bank)
        {
            var restaurant = _session.GetInt32("restaurantsessionid");

            if (ModelState.IsValid)
            {
                if (restaurant != null)
                {
                    bank.RestaurantId = restaurant;

                    var allBanks = await _db.Banks.ToListAsync();
                    if (allBanks.Any(b => b.RestaurantId == restaurant && b.Name == bank.Name))
                    {
                        TempData["bank"] = "You cannot add this Bank because it already exist!!!";
                        TempData["notificationType"] = NotificationType.Error.ToString();
                        return RedirectToAction("Index");
                    }

                    await _db.AddAsync(bank);
                    await _db.SaveChangesAsync();

                    TempData["bank"] = "You have successfully added a new Bank!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();

                    return Json(new { success = true });
                }
                else
                {
                    TempData["bank"] = "Session Expired, Login Again";
                    TempData["notificationtype"] = NotificationType.Info.ToString();
                    return RedirectToAction("Restaurant", "Access");
                }
            }
            return View("Index");
        }
        #endregion

        #region Bank Edit

        //GET: Banks/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _db.Banks.SingleOrDefaultAsync(b => b.BankId == id);

            if (bank == null)
            {
                return NotFound();
            }
            return PartialView("Edit", bank);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bank bank)
        {
            if (id != bank.BankId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var restaurant = _session.GetInt32("restaurantsessionid");

                    if (restaurant != null)
                    {
                        bank.RestaurantId = restaurant;
                    }
                    _db.Update(bank);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankExists(bank.BankId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["bank"] = "You have successfully modified a Bank!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Bank Delete

        //GET: Banks/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bank = await _db.Banks.SingleOrDefaultAsync(b => b.BankId == id);

            if (bank == null)
            {
                return NotFound();
            }

            return PartialView("Delete", bank);
        }

        //POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bank = await _db.Banks.SingleOrDefaultAsync(b => b.BankId == id);
            if(bank != null)
            {
                _db.Banks.Remove(bank);
                await _db.SaveChangesAsync();

                TempData["bank"] = "You have successfully deleted a Bank!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Bank Exists

        private bool BankExists(int id)
        {
            return _db.Banks.Any(b => b.BankId == id);
        }

        #endregion

    }
}
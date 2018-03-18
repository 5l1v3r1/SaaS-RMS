using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        #region Constructor

        public BanksController(ApplicationDbContext context)
        {
            _db = context;
        }

        #endregion

        #region Bank Index

        public async Task <IActionResult> Index()
        {
            return View(await _db.Banks.ToListAsync());
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
        public async Task<IActionResult> Create(int id, Bank bank)
        {
            if (ModelState.IsValid)
            {
                var allBanks = _db.Banks.ToList();
                if (allBanks.Any(b => b.Name == bank.Name))
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
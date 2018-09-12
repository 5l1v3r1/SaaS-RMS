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
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.SystemControllers
{
    public class SubscriptionsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public SubscriptionsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Index

        // GET: Subscriptions
        public async Task<IActionResult> Index(int PackageId)
        {
            try
            {
                var subscriptions = _db.Subcriptions.Where(s => s.PackageId == PackageId);
                
                if (subscriptions == null)
                {
                    return NotFound();
                }

                _session.SetInt32("packageid", PackageId);
                return View(await subscriptions.ToListAsync());
            }
            catch(Exception e)
            {
                return Json(e);
            }
        }

        #endregion

        #region Create

        // GET: Subscriptions/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["packageid"] = _session.GetInt32("packageid");
            
            var subscription = new Subscription();
            return View("Create", subscription);
        }

        // POST: Subscriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubscriptionId,StartDate,EndDate,PackageId")] Subscription subscription)
        {
            if (ModelState.IsValid)
            {
                var packageid = _session.GetInt32("packageid");
                var _package = await _db.Packages.FindAsync(packageid);

                if (subscription.Duration == Models.Enums.RestauarantSubscriptionDuration.OneMonth)
                {
                    var _newAmount = _package.Amount * 1;
                    subscription.Amount = _newAmount;
                }

                else if (subscription.Duration == Models.Enums.RestauarantSubscriptionDuration.SixMonths)
                {
                    var _newAmount = _package.Amount * 5;
                    subscription.Amount = _newAmount;
                }

                else if (subscription.Duration == Models.Enums.RestauarantSubscriptionDuration.TwelveMonths)
                {
                    var _newAmount = _package.Amount * 10;
                    subscription.Amount = _newAmount;
                }

                var allSubscriptions = await _db.Subcriptions.ToListAsync();
                if (allSubscriptions.Any(s => s.Duration == subscription.Duration))
                {
                    TempData["subscriptions"] = "You cannot the subscription because it already exist!!!";
                    TempData["notificationType"] = NotificationType.Error.ToString();
                    return RedirectToAction("Index", new { PackageId = subscription.PackageId });
                }

                _db.Add(subscription);
                await _db.SaveChangesAsync();
                return Json(new { success = true });

            }
            return RedirectToAction(nameof(Index), new { PackageId = subscription.PackageId });
        }

        #endregion

        // GET: Subscriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _db.Subcriptions
                .Include(s => s.Package)
                .SingleOrDefaultAsync(m => m.SubscriptionId == id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        





        // GET: Subscriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _db.Subcriptions.SingleOrDefaultAsync(m => m.SubscriptionId == id);
            if (subscription == null)
            {
                return NotFound();
            }
            ViewData["PackageId"] = new SelectList(_db.Packages, "PackageId", "Description", subscription.PackageId);
            return View(subscription);
        }

        // POST: Subscriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubscriptionId,StartDate,EndDate,PackageId")] Subscription subscription)
        {
            if (id != subscription.SubscriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(subscription);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubscriptionExists(subscription.SubscriptionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PackageId"] = new SelectList(_db.Packages, "PackageId", "Description", subscription.PackageId);
            return View(subscription);
        }

        // GET: Subscriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subscription = await _db.Subcriptions
                .Include(s => s.Package)
                .SingleOrDefaultAsync(m => m.SubscriptionId == id);
            if (subscription == null)
            {
                return NotFound();
            }

            return View(subscription);
        }

        // POST: Subscriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscription = await _db.Subcriptions.SingleOrDefaultAsync(m => m.SubscriptionId == id);
            _db.Subcriptions.Remove(subscription);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubscriptionExists(int id)
        {
            return _db.Subcriptions.Any(e => e.SubscriptionId == id);
        }
    }
}

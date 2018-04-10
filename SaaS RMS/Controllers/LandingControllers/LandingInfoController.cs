using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Enities.Landing;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.LandingControllers
{
    public class LandingInfoController : Controller
    {
        private readonly ApplicationDbContext _db;

        #region Constructor

        public LandingInfoController(ApplicationDbContext context)
        {
            _db = context;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            var landingInfo = await _db.LandingInfo.ToListAsync();
            return View(landingInfo);
        }

        #endregion

        #region Create

        //GET: LandingInfo/Create
        [HttpGet]
        public IActionResult Create()
        {
            var landingInfo = new LandingInfo();
            return PartialView("Create", landingInfo);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LandingInfo landingInfo)
        {
            if (ModelState.IsValid)
            {
                var approval = await _db.LandingInfo.ToListAsync();

                if(approval.Any(l => l.Approval == ApprovalEnum.Apply))
                {
                    TempData["landinginfo"] = "The Landing information wasn't added because a Landing Information is already applied!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();

                    return RedirectToAction("Index");
                }

                await _db.AddAsync(landingInfo);
                await _db.SaveChangesAsync();

                TempData["landinginfo"] = "You have successfully added a new Landing Information!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();
                
                return Json(new { success = true });
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Edit

        //GET: LandingInfo/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landingInfo = await _db.LandingInfo.SingleOrDefaultAsync(l => l.LandingInfoId == id);

            if (landingInfo == null)
            {
                return NotFound();
            }

            return PartialView("Edit", landingInfo);
        }

        //POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, LandingInfo landingInfo)
        {
            if(landingInfo.LandingInfoId != id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var approval = await _db.LandingInfo.ToListAsync();

                    if (approval.Any(l => l.Approval == ApprovalEnum.Apply))
                    {
                        TempData["landinginfo"] = "The Landing information wasn't added because a Landing Information is already applied!!!";
                        TempData["notificationType"] = NotificationType.Success.ToString();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        _db.Update(landingInfo);
                        await _db.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LandingInfoExists(landingInfo.LandingInfoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                TempData["landinginfo"] = "You have successfully modified a Landing Information!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Delete

        //GET: LandingInfo/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var landingInfo = await _db.LandingInfo.SingleOrDefaultAsync(b => b.LandingInfoId == id);

            if (landingInfo == null)
            {
                return NotFound();
            }

            return PartialView("Delete", landingInfo);
        }

        //POST:
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var landingInfo = await _db.LandingInfo.SingleOrDefaultAsync(b => b.LandingInfoId == id);
            if (landingInfo != null)
            {
                _db.LandingInfo.Remove(landingInfo);
                await _db.SaveChangesAsync();

                TempData["landingInfo"] = "You have successfully deleted a Landing Information!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region LandingInfo Exists

        private bool LandingInfoExists(int id)
        {
            return _db.LandingInfo.Any(b => b.LandingInfoId == id);
        }

        #endregion

    }
}
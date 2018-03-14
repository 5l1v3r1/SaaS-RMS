using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;

namespace SaaS_RMS.Controllers.SystemControllers
{
    public class LgasController : Controller
    {
        private readonly ApplicationDbContext _db;

        #region Constructor

        public LgasController(ApplicationDbContext context)
        {
            _db = context;
        }

        #endregion

        #region Lga Index

        // GET: Lgas
        [Route("lgas/index/{StateId}")]
        public async Task<IActionResult> Index(int StateId)
        {
            try
            {
                var lga = _db.Lgas.Where(l => l.StateId == StateId);
                if(lga != null)
                {
                    HttpContext.Session.SetInt32("SId", StateId);
                    return View(await lga.ToListAsync());
                }
            }
            catch(Exception e)
            {
                return Json(e);
            }
            return View();
        }

        #endregion

        #region Lga Details

        // GET: Lgas/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var lga = await _db.Lgas
        //        .Include(l => l.State)
        //        .SingleOrDefaultAsync(m => m.LgaId == id);
        //    if (lga == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(lga);
        //}

        #endregion

        #region Lga Create

        // GET: Lgas/Create
        public IActionResult Create()
        {
            ViewData["SId"] = Convert.ToInt32(HttpContext.Session.GetInt32("SId"));
            var lga = new Lga();
            return PartialView("Create", lga);
        }

        // POST: Lgas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LgaId,Name,StateId")] Lga lga)
        {
            if (ModelState.IsValid)
            {
                await _db.AddAsync(lga);
                await _db.SaveChangesAsync();

                TempData["lga"] = "You have successfully added a new Local Government Area!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
                
            }
            return View("Index", new { StateId = lga.StateId });
        }

        #endregion

        #region Lga Edit

        // GET: Lgas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lga = await _db.Lgas.SingleOrDefaultAsync(m => m.LgaId == id);
            if (lga == null)
            {
                return NotFound();
            }
            return View("Edit", lga);
        }

        // POST: Lgas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LgaId,Name,StateId")] Lga lga)
        {
            if (id != lga.LgaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(lga);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LgaExists(lga.LgaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { success = true });

            }
            return RedirectToAction("Index", new { StateId = lga.StateId });
        }

        #endregion

        #region Lga Delete

        // GET: Lgas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lga = await _db.Lgas
                .Include(l => l.State)
                .SingleOrDefaultAsync(m => m.LgaId == id);
            if (lga == null)
            {
                return NotFound();
            }

            return PartialView("Delete", lga);
        }

        // POST: Lgas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lga = await _db.Lgas.SingleOrDefaultAsync(m => m.LgaId == id);
            if (lga != null)
            {
                _db.Lgas.Remove(lga);
                await _db.SaveChangesAsync();
                return Json(new { success = true });
            }
            return RedirectToAction("Index", new { StateId = lga.StateId });
        }

        #endregion

        #region Lga Exists

        private bool LgaExists(int id)
        {
            return _db.Lgas.Any(e => e.LgaId == id);
        }

        #endregion


    }
}

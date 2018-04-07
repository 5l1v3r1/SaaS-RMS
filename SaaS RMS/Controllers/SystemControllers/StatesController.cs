using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.System;
using SaaS_RMS.Models.Enums;

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

        #region States Index

        // GET: States
        public async Task<IActionResult> Index()
        {
            return View(await _db.States.ToListAsync());
        }

        #endregion

        #region States Create

        // GET: States/Create
        public IActionResult Create()
        {
            var state = new State();
            return PartialView("Create", state);
        }

        // POST: States/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateId,Name")] State state)
        {
            if (ModelState.IsValid)
            {
                var allStates = _db.States.ToList();
                if (allStates.Any(s => s.Name == state.Name))
                {
                    TempData["state"] = "You cannot add " + state.Name + " state because it already exist!!!";
                    TempData["notificationType"] = NotificationType.Error.ToString();
                    return RedirectToAction("Index", state);
                }

                _db.Add(state);
                await _db.SaveChangesAsync();

                TempData["state"] = "You have successfully added " + state.Name + " as a new State!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();

                return Json(new { success = true });
            }
            return RedirectToAction("Index", state);
        }

        #endregion

        #region States Edit

        // GET: States/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _db.States.SingleOrDefaultAsync(m => m.StateId == id);
            if (state == null)
            {
                return NotFound();
            }
            return PartialView("Edit", state);
        }

        // POST: States/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateId,Name")] State state)
        {
            if (id != state.StateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(state);
                    await _db.SaveChangesAsync();
                    TempData["state"] = "You have successfully modified " + state.Name + " State!!!";
                    TempData["notificationType"] = NotificationType.Success.ToString();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateExists(state.StateId))
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
            return View(state);
        }

        #endregion

        #region States Delete

        // GET: States/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _db.States
                .SingleOrDefaultAsync(m => m.StateId == id);
            if (state == null)
            {
                return NotFound();
            }

            return PartialView("Delete", state);
        }

        // POST: States/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var state = await _db.States.SingleOrDefaultAsync(m => m.StateId == id);

            if (state != null)
            {
                _db.States.Remove(state);
                await _db.SaveChangesAsync();
                TempData["state"] = "You have successfully deleted " + state.Name + " State!!!";
                TempData["notificationType"] = NotificationType.Success.ToString();
                return Json(new { success = true });
            }
            
            return RedirectToAction("Index", state);
        }

        #endregion

        #region State Exists

        private bool StateExists(int id)
        {
            return _db.States.Any(e => e.StateId == id);
        }

        #endregion

        #region States Details

        // GET: States/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var state = await _db.States
        //        .SingleOrDefaultAsync(m => m.StateId == id);
        //    if (state == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(state);
        //}

        #endregion
        
    }
}

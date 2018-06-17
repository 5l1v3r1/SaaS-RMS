using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaaS_RMS.Data;
using SaaS_RMS.Models.Entities.Vendor;

namespace SaaS_RMS.Controllers.VendorControllers
{
    public class CompanyVendorsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        #region Constructor

        public CompanyVendorsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _db = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        // GET: CompanyVendors
        public async Task<IActionResult> Index()
        {
            return View(await _db.CompanyVendors.ToListAsync());
        }

        // GET: CompanyVendors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyVendor = await _db.CompanyVendors
                .SingleOrDefaultAsync(m => m.CompanyVendorId == id);
            if (companyVendor == null)
            {
                return NotFound();
            }

            return View(companyVendor);
        }

        // GET: CompanyVendors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyVendors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyVendorId,Name,Address,ContactNumber,OfficeNumber,VendorItem,Request")] CompanyVendor companyVendor)
        {
            if (ModelState.IsValid)
            {
                _db.Add(companyVendor);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyVendor);
        }

        // GET: CompanyVendors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyVendor = await _db.CompanyVendors.SingleOrDefaultAsync(m => m.CompanyVendorId == id);
            if (companyVendor == null)
            {
                return NotFound();
            }
            return View(companyVendor);
        }

        // POST: CompanyVendors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CompanyVendorId,Name,Address,ContactNumber,OfficeNumber,VendorItem,Request")] CompanyVendor companyVendor)
        {
            if (id != companyVendor.CompanyVendorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(companyVendor);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyVendorExists(companyVendor.CompanyVendorId))
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
            return View(companyVendor);
        }

        // GET: CompanyVendors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyVendor = await _db.CompanyVendors
                .SingleOrDefaultAsync(m => m.CompanyVendorId == id);
            if (companyVendor == null)
            {
                return NotFound();
            }

            return View(companyVendor);
        }

        // POST: CompanyVendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyVendor = await _db.CompanyVendors.SingleOrDefaultAsync(m => m.CompanyVendorId == id);
            _db.CompanyVendors.Remove(companyVendor);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyVendorExists(int id)
        {
            return _db.CompanyVendors.Any(e => e.CompanyVendorId == id);
        }
    }
}

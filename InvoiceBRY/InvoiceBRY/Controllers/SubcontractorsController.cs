using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceBRY.Models;

namespace InvoiceBRY.Controllers
{
    public class SubcontractorsController : Controller
    {
        private readonly InvoiceContext _context;

        public SubcontractorsController(InvoiceContext context)
        {
            _context = context;
        }

        // GET: Subcontractors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Subcontractors.ToListAsync());
        }

        // GET: Subcontractors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcontractor = await _context.Subcontractors
                .FirstOrDefaultAsync(m => m.SubcontractorId == id);
            if (subcontractor == null)
            {
                return NotFound();
            }

            return View(subcontractor);
        }

        // GET: Subcontractors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subcontractors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubcontractorId,CompanyName,FirstName,MiddleName,LastName,PhoneNumber,Email,Country,Address,AptUnitOrSuit,City,State,HstAmount")] Subcontractor subcontractor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subcontractor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subcontractor);
        }

        // GET: Subcontractors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcontractor = await _context.Subcontractors.FindAsync(id);
            if (subcontractor == null)
            {
                return NotFound();
            }
            return View(subcontractor);
        }

        // POST: Subcontractors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubcontractorId,CompanyName,FirstName,MiddleName,LastName,PhoneNumber,Email,Country,Address,AptUnitOrSuit,City,State,HstAmount")] Subcontractor subcontractor)
        {
            if (id != subcontractor.SubcontractorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subcontractor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubcontractorExists(subcontractor.SubcontractorId))
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
            return View(subcontractor);
        }

        // GET: Subcontractors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subcontractor = await _context.Subcontractors
                .FirstOrDefaultAsync(m => m.SubcontractorId == id);
            if (subcontractor == null)
            {
                return NotFound();
            }

            return View(subcontractor);
        }

        // POST: Subcontractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subcontractor = await _context.Subcontractors.FindAsync(id);
            _context.Subcontractors.Remove(subcontractor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubcontractorExists(int id)
        {
            return _context.Subcontractors.Any(e => e.SubcontractorId == id);
        }
    }
}

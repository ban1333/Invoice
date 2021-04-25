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
    public class InvoiceDescriptionsController : Controller
    {
        private readonly InvoiceContext _context;

        public InvoiceDescriptionsController(InvoiceContext context)
        {
            _context = context;
        }

        // GET: InvoiceDescriptions
        public async Task<IActionResult> Index(int? invoiceId, string? name, int? subcontractorId, int? HST)
        {
            if (invoiceId == null) {
                invoiceId = (int)TempData["invoiceId"];
                ViewData["name"] = TempData["name"];
                ViewData["subcontractorId"] = TempData["subcontractorId"];
                ViewData["HST"] = TempData["HST"];
            }
            else
            {
                ViewData["name"] = name;
                ViewData["subcontractorId"] = subcontractorId;
                ViewData["HST"] = HST;
            }

            return View(await _context.InvoiceDescriptions.Where(i => i.InvoiceId == invoiceId).ToListAsync());
        }

        // GET: InvoiceDescriptions
        public async Task<IActionResult> SubcontractorIndex(int? invoiceId, string? name, int? subcontractorId, int? HST)
        {
            if (invoiceId == null)
            {
                invoiceId = (int)TempData["invoiceId"];
                ViewData["name"] = TempData["name"];
                ViewData["subcontractorId"] = TempData["subcontractorId"];
                ViewData["HST"] = TempData["HST"];
            }
            else
            {
                ViewData["invoiceId"] = invoiceId;
                ViewData["name"] = name;
                ViewData["subcontractorId"] = subcontractorId;
                ViewData["HST"] = HST;
            }

            return View("SubcontractorIndex", await _context.InvoiceDescriptions.Where(i => i.InvoiceId == invoiceId).ToListAsync());
        }

        // GET: InvoiceDescriptions
        public async Task<IActionResult> CustomerIndex(int? invoiceId, string? name, int? customerId, int? HST)
        {
            if (invoiceId == null)
            {
                invoiceId = (int)TempData["invoiceId"];
                ViewData["name"] = TempData["name"];
                ViewData["customerId"] = TempData["customerId"];
                ViewData["HST"] = TempData["HST"];
            }
            else
            {
                ViewData["invoiceId"] = invoiceId;
                ViewData["name"] = name;
                ViewData["customerId"] = customerId;
                ViewData["HST"] = HST;
            }

            return View("CustomerIndex", await _context.InvoiceDescriptions.Where(i => i.InvoiceId == invoiceId).ToListAsync());
        }

        // GET: InvoiceDescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDescription = await _context.InvoiceDescriptions
                .FirstOrDefaultAsync(m => m.DescriptionId == id);
            if (invoiceDescription == null)
            {
                return NotFound();
            }

            return View(invoiceDescription);
        }

        // GET: InvoiceDescriptions/Details/5
        public async Task<IActionResult> CustomerDetails(int? id, int? invoiceId, string? name, int? customerId, int? HST)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDescription = await _context.InvoiceDescriptions
                .FirstOrDefaultAsync(m => m.DescriptionId == id);
            if (invoiceDescription == null)
            {
                return NotFound();
            }
            ViewData["invoiceId"] = invoiceId;
            ViewData["name"] = name;
            ViewData["customerId"] = customerId;
            ViewData["HST"] = HST;

            return View(invoiceDescription);
        }

        // GET: InvoiceDescriptions/Details/5
        public async Task<IActionResult> SubcontractorDetails(int? id, int? invoiceId, string? name, int? subcontractorId, int? HST)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDescription = await _context.InvoiceDescriptions
                .FirstOrDefaultAsync(m => m.DescriptionId == id);
            if (invoiceDescription == null)
            {
                return NotFound();
            }
            ViewData["invoiceId"] = invoiceId;
            ViewData["name"] = name;
            ViewData["subcontractorId"] = subcontractorId;
            ViewData["HST"] = HST;

            return View(invoiceDescription);
        }

        // GET: InvoiceDescriptions/Create
        public IActionResult Create(int? invoiceId)
        {
            return View();
        }

        // GET: InvoiceDescriptions/CreateCustomer
        public IActionResult CreateCustomer(int? invoiceId, string? name, int? customerId, int? HST)
        {
            ViewData["invoiceId"] = invoiceId;
            ViewData["name"] = name;
            ViewData["customerId"] = customerId;
            ViewData["HST"] = HST;
            return View("CreateCustomer");
        }

        // POST: InvoiceDescriptions/CreateCustomer
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer([Bind("DescriptionId,InvoiceId,Description")] InvoiceDescription invoiceDescription, string? name, int? customerId, int? HST)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceDescription);
                await _context.SaveChangesAsync();

                TempData["name"] = name;
                TempData["customerId"] = customerId;
                TempData["HST"] = HST;
                TempData["invoiceId"] = invoiceDescription.InvoiceId;
                return RedirectToAction(nameof(CustomerIndex));
            }
            return View(invoiceDescription);
        }

        // GET: InvoiceDescriptions/CreateSubcontractor
        public IActionResult CreateSubcontractor(int? invoiceId, string? name, int? subcontractorId, int? HST)
        {
            ViewData["invoiceId"] = invoiceId;
            ViewData["name"] = name;
            ViewData["subcontractorId"] = subcontractorId;
            ViewData["HST"] = HST;
            return View("CreateSubcontractor");
        }

        // POST: InvoiceDescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubcontractor([Bind("DescriptionId,InvoiceId,Description")] InvoiceDescription invoiceDescription, string? name, int? subcontractorId, int? HST)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceDescription);
                await _context.SaveChangesAsync();

                TempData["name"] = name;
                TempData["subcontractorId"] = subcontractorId;
                TempData["HST"] = HST;
                TempData["invoiceId"] = invoiceDescription.InvoiceId;
                return RedirectToAction(nameof(SubcontractorIndex));
            }
            return View(invoiceDescription);
        }

        // GET: InvoiceDescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDescription = await _context.InvoiceDescriptions.FindAsync(id);
            if (invoiceDescription == null)
            {
                return NotFound();
            }
            return View(invoiceDescription);
        }

        // GET: InvoiceDescriptions/EditCustomer/5
        public async Task<IActionResult> EditCustomer(int? id, int? invoiceId, string? name, int? customerId, int? HST)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDescription = await _context.InvoiceDescriptions.FindAsync(id);
            if (invoiceDescription == null)
            {
                return NotFound();
            }
            ViewData["invoiceId"] = invoiceId;
            ViewData["name"] = name;
            ViewData["customerId"] = customerId;
            ViewData["HST"] = HST;
            return View("EditCustomer", invoiceDescription);
        }

        // POST: InvoiceDescriptions/EditCustomer/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(int id, [Bind("DescriptionId,InvoiceId,Description")] InvoiceDescription invoiceDescription, string? name, int? customerId, int? HST)
        {
            if (id != invoiceDescription.DescriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceDescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceDescriptionExists(invoiceDescription.DescriptionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["name"] = name;
                TempData["customerId"] = customerId;
                TempData["HST"] = HST;
                TempData["invoiceId"] = invoiceDescription.InvoiceId;
                return RedirectToAction(nameof(CustomerIndex));
            }
            return View("EditCustomer", invoiceDescription);
        }

        // GET: InvoiceDescriptions/Edit/5
        public async Task<IActionResult> EditSubcontractor(int? id, int? invoiceId, string? name, int? subcontractorId, int? HST)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDescription = await _context.InvoiceDescriptions.FindAsync(id);
            if (invoiceDescription == null)
            {
                return NotFound();
            }
            ViewData["invoiceId"] = invoiceId;
            ViewData["name"] = name;
            ViewData["subcontractorId"] = subcontractorId;
            ViewData["HST"] = HST;
            return View(invoiceDescription);
        }

        // POST: InvoiceDescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubcontractor(int id, [Bind("DescriptionId,InvoiceId,Description")] InvoiceDescription invoiceDescription, string? name, int? subcontractorId, int? HST)
        {
            if (id != invoiceDescription.DescriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceDescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceDescriptionExists(invoiceDescription.DescriptionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["name"] = name;
                TempData["subcontractorId"] = subcontractorId;
                TempData["HST"] = HST;
                TempData["invoiceId"] = invoiceDescription.InvoiceId;
                return RedirectToAction(nameof(SubcontractorIndex));
            }
            return View(invoiceDescription);
        }

        // POST: InvoiceDescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DescriptionId,InvoiceId,Description")] InvoiceDescription invoiceDescription)
        {
            if (id != invoiceDescription.DescriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceDescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceDescriptionExists(invoiceDescription.DescriptionId))
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
            return View(invoiceDescription);
        }

        // GET: InvoiceDescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDescription = await _context.InvoiceDescriptions
                .FirstOrDefaultAsync(m => m.DescriptionId == id);
            if (invoiceDescription == null)
            {
                return NotFound();
            }

            return View(invoiceDescription);
        }

        // GET: InvoiceDescriptions/Delete/5
        public async Task<IActionResult> DeleteCustomer(int? id, int? invoiceId, string? name, int? customerId, int? HST)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDescription = await _context.InvoiceDescriptions
                .FirstOrDefaultAsync(m => m.DescriptionId == id);
            if (invoiceDescription == null)
            {
                return NotFound();
            }
            ViewData["invoiceId"] = invoiceId;
            ViewData["name"] = name;
            ViewData["customerId"] = customerId;
            ViewData["HST"] = HST;
            return View("DeleteCustomer", invoiceDescription);
        }

        // POST: InvoiceDescriptions/Delete/5
        [HttpPost, ActionName("DeleteSubcontractor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedCustomer(int id, string? name, int? customerId, int? HST)
        {
            var invoiceDescription = await _context.InvoiceDescriptions.FindAsync(id);
            _context.InvoiceDescriptions.Remove(invoiceDescription);
            await _context.SaveChangesAsync();
            TempData["name"] = name;
            TempData["customerId"] = customerId;
            TempData["HST"] = HST;
            TempData["invoiceId"] = invoiceDescription.InvoiceId;
            return RedirectToAction(nameof(CustomerIndex));
        }

        // GET: InvoiceDescriptions/Delete/5
        public async Task<IActionResult> DeleteSubcontractor(int? id, int? invoiceId, string? name, int? subcontractorId, int? HST)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDescription = await _context.InvoiceDescriptions
                .FirstOrDefaultAsync(m => m.DescriptionId == id);
            if (invoiceDescription == null)
            {
                return NotFound();
            }
            ViewData["invoiceId"] = invoiceId;
            ViewData["name"] = name;
            ViewData["subcontractorId"] = subcontractorId;
            ViewData["HST"] = HST;
            return View(invoiceDescription);
        }

        // POST: InvoiceDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string? name, int? subcontractorId, int? HST)
        {
            var invoiceDescription = await _context.InvoiceDescriptions.FindAsync(id);
            _context.InvoiceDescriptions.Remove(invoiceDescription);
            await _context.SaveChangesAsync();
            TempData["name"] = name;
            TempData["subcontractorId"] = subcontractorId;
            TempData["HST"] = HST;
            TempData["invoiceId"] = invoiceDescription.InvoiceId;
            return RedirectToAction(nameof(Index));
        }

        // POST: InvoiceDescriptions/Delete/5
        [HttpPost, ActionName("DeleteSubcontractor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedSubcontractor(int id, string? name, int? subcontractorId, int? HST)
        {
            var invoiceDescription = await _context.InvoiceDescriptions.FindAsync(id);
            _context.InvoiceDescriptions.Remove(invoiceDescription);
            await _context.SaveChangesAsync();
            TempData["name"] = name;
            TempData["subcontractorId"] = subcontractorId;
            TempData["HST"] = HST;
            TempData["invoiceId"] = invoiceDescription.InvoiceId;
            return RedirectToAction(nameof(SubcontractorIndex));
        }

        private bool InvoiceDescriptionExists(int id)
        {
            return _context.InvoiceDescriptions.Any(e => e.DescriptionId == id);
        }
    }
}

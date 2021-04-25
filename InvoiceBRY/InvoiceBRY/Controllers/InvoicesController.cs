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
    public class InvoicesController : Controller
    {
        private readonly InvoiceContext _context;

        public InvoicesController(InvoiceContext context)
        {
            _context = context;
        }

        // GET: Invoice
        public async Task<IActionResult> ViewInvoice(int customerId, string name)
        {
            var invoiceContext = _context.Invoices.Include(i => i.Customer).Where(i => i.CustomerId == customerId);

            return View("InvoiceCustomer", await invoiceContext.ToListAsync());
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var invoiceContext = _context.Invoices.Include(i => i.Customer).Include(i => i.Subcontractor);
            return View(await invoiceContext.ToListAsync());
        }

        //GET: customer invoice
        public async Task<IActionResult> CustomerIndex(int? customerId, string? firstName, string? lastName)
        {
            
            ViewData["name"] = firstName + " " + lastName;
            ViewData["customerId"] = customerId;
            if(customerId == null)
            {
                ViewData["name"] = TempData["name"];
                ViewData["customerId"] = TempData["customerId"];
                customerId = (int)TempData["customerId"];
            }
            var invoiceContext = _context.Invoices.Include(i => i.Customer).Where(i => i.CustomerId == customerId);
            return View("IndexCustomer", await invoiceContext.ToListAsync());
        }

        //GET: subcontractor invoice
        public async Task<IActionResult> SubcontractorIndex(int? subcontractorId, string? firstName, string? lastName, int? HST)
        {
            
            ViewData["name"] = firstName + " " + lastName;
            ViewData["subcontractorId"] = subcontractorId;
            ViewData["HST"] = HST;
            if(subcontractorId == null)
            {
                ViewData["name"] = TempData["name"];
                ViewData["subcontractorId"] = TempData["subcontractorId"];
                subcontractorId = (int)TempData["subcontractorId"];
            }
            var invoiceContext = _context.Invoices.Include(i => i.Subcontractor).Where(i => i.SubcontractorId == subcontractorId);
            return View("IndexSubcontractor", await invoiceContext.ToListAsync());
        }
        
        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Subcontractor)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> CustomerDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName");
            ViewData["SubcontractorId"] = new SelectList(_context.Subcontractors, "SubcontractorId", "FirstName");
            return View();
        }

        //CreateCustomer
        public IActionResult CreateCustomer(int? CustomerId)
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName");
            return View("CreateCustomer");
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer([Bind("InvoiceId,CustomerId,SubcontractorId,IsPaid,Date,NumberOfHours,Rate,SubTotal,Tax,TotalAmount")] Invoice invoice, string name)
        {
            invoice.SubTotal = invoice.NumberOfHours * invoice.Rate;
            decimal tax = invoice.Tax / 100;
            tax += 1;
            invoice.TotalAmount = invoice.SubTotal * tax;
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                TempData["name"] = name;
                TempData["customerId"] = invoice.CustomerId;
                return RedirectToAction(nameof(CustomerIndex));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", invoice.CustomerId);
            return View(invoice);
        }

        //CreateSubcontractor
        public IActionResult CreateSubcontractor(int? subcontractorId, int? HST)
        {
            ViewData["subcontractorId"] = new SelectList(_context.Subcontractors.Where(i => i.SubcontractorId == subcontractorId), "SubcontractorId", "FirstName");
            ViewData["HST"] = HST;
            return View("CreateSubcontractor");
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceId,CustomerId,SubcontractorId,IsPaid,Date,NumberOfHours,Rate,SubTotal,Tax,TotalAmount")] Invoice invoice, string name)
        {
            invoice.SubTotal = invoice.NumberOfHours * invoice.Rate;
            decimal tax = invoice.Tax / 100;
            tax += 1;
            invoice.TotalAmount = invoice.SubTotal * tax;
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                TempData["name"] = name;
                TempData["subcontractorId"] = invoice.SubcontractorId;
                return RedirectToAction(nameof(SubcontractorIndex));
            }
            ViewData["SubcontractorId"] = new SelectList(_context.Subcontractors, "SubcontractorId", "FirstName", invoice.SubcontractorId);
            return View(invoice);
        }

        // GET: Invoices/EditCustomer/5
        public async Task<IActionResult> EditCustomer(int? id, string? name)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", invoice.CustomerId);
            ViewData["name"] = name;
            return View(invoice);
        }

        // POST: Invoices/EditCustomer/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(int id, [Bind("InvoiceId,CustomerId,SubcontractorId,IsPaid,Date,NumberOfHours,Rate,SubTotal,Tax,TotalAmount")] Invoice invoice, string? name)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["name"] = name;
                TempData["customerId"] = id;
                return RedirectToAction(nameof(CustomerIndex));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", invoice.CustomerId);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", invoice.CustomerId);
            ViewData["SubcontractorId"] = new SelectList(_context.Subcontractors, "SubcontractorId", "FirstName", invoice.SubcontractorId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceId,CustomerId,SubcontractorId,IsPaid,Date,NumberOfHours,Rate,SubTotal,Tax,TotalAmount")] Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FirstName", invoice.CustomerId);
            ViewData["SubcontractorId"] = new SelectList(_context.Subcontractors, "SubcontractorId", "FirstName", invoice.SubcontractorId);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Subcontractor)
                .FirstOrDefaultAsync(m => m.InvoiceId == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.InvoiceId == id);
        }
    }
}

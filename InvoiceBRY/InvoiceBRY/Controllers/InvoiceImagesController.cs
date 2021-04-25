using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InvoiceBRY.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace InvoiceBRY.Controllers
{
    public class InvoiceImagesController : Controller
    {
        private readonly InvoiceContext _context;
        private readonly IWebHostEnvironment _env;

        public InvoiceImagesController(InvoiceContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: InvoiceImages
        public async Task<IActionResult> Index(int? invoiceId, string? name, int? subcontractorId, int? HST)
        {
            if(name == null)
            {
                ViewData["name"] = TempData["name"];
                ViewData["subcontractorId"] = TempData["subcontractorId"];
                ViewData["HST"] = TempData["HST"];
                ViewData["invoiceId"] = TempData["invoiceId"];
                invoiceId = (int)TempData["invoiceId"];
            }
            else
            {
                try
                {
                    ViewData["name"] = name;
                    ViewData["invoiceId"] = invoiceId;
                    ViewData["subcontractorId"] = subcontractorId;
                    ViewData["HST"] = HST;
                }
                catch (Exception)
                {

                }
            }
            return View(await _context.InvoiceImages.Where(i => i.InvoiceId == invoiceId).ToListAsync());
        }

        // GET: InvoiceImages
        public async Task<IActionResult> CustomerIndex(int? invoiceId, string? name, int? customerId, int? HST)
        {
            if (name == null)
            {
                ViewData["name"] = TempData["name"];
                ViewData["customerId"] = TempData["customerId"];
                ViewData["HST"] = TempData["HST"];
                ViewData["invoiceId"] = TempData["invoiceId"];
                invoiceId = (int)TempData["invoiceId"];
            }
            else
            {
                try
                {
                    ViewData["name"] = name;
                    ViewData["invoiceId"] = invoiceId;
                    ViewData["customerId"] = customerId;
                    ViewData["HST"] = HST;
                }
                catch (Exception)
                {

                }
            }
            return View(await _context.InvoiceImages.Where(i => i.InvoiceId == invoiceId).ToListAsync());
        }

        // GET: InvoiceImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceImage = await _context.InvoiceImages
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (invoiceImage == null)
            {
                return NotFound();
            }

            return View(invoiceImage);
        }

        // GET: InvoiceImages/Create
        public IActionResult Create(int? invoiceId, string? name, int? subcontractorId, int? HST)
        {
            ViewData["invoiceId"] = invoiceId;
            ViewData["name"] = name;
            ViewData["subcontractorId"] = subcontractorId;
            ViewData["HST"] = HST;

            return View();
        }

        // POST: InvoiceImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile Image, int invoiceId, string? name, int? subcontractorId, int? HST)
        {//[Bind("ImageId,InvoiceId,Image")] InvoiceImage invoiceImage
            try
            {
                InvoiceImage image = new InvoiceImage();

                string imageExtension = Path.GetExtension(Image.FileName);

                string[] path = new string[] { _env.WebRootPath, "images", "upload", Image.FileName };
                var saveImage = Path.Combine(path);
                

                if (imageExtension == ".jpg" || imageExtension == ".jpeg" || imageExtension == ".png")
                {
                    using (var uploadImage = new FileStream(saveImage, FileMode.Create))
                    {
                        await Image.CopyToAsync(uploadImage);
                    }
                }
                else
                {
                    //add viewdata for everything
                    ViewData["DANGER"] = "Please have the extension be .jpg .jpeg or .png";
                    return View();
                }

                //add it to the database
                image.InvoiceId = invoiceId;
                image.Image = Image.FileName;
                _context.Add(image);
                await _context.SaveChangesAsync();

                if (subcontractorId != null)
                {
                    //IF IT'S A SUBCONTRACTOR
                    TempData["name"] = name;
                    TempData["subcontractorId"] = subcontractorId;
                    TempData["HST"] = HST;
                    TempData["invoiceId"] = invoiceId;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //IF IT'S A CUSTOMER

                }


            }
            catch(Exception)
            {
                //return View(invoiceImage);
            }

            //return View(invoiceImage);
            return View();
        }

        // GET: InvoiceImages/Create
        public IActionResult CreateCustomer(int? invoiceId, string? name, int? customerId, int? HST)
        {
            ViewData["invoiceId"] = invoiceId;
            ViewData["name"] = name;
            ViewData["customerId"] = customerId;
            ViewData["HST"] = HST;

            return View("CreateCustomer");
        }

        // POST: InvoiceImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer(IFormFile Image, int invoiceId, string? name, int? customerId, int? HST)
        {//[Bind("ImageId,InvoiceId,Image")] InvoiceImage invoiceImage
            try
            {
                InvoiceImage image = new InvoiceImage();

                string imageExtension = Path.GetExtension(Image.FileName);

                string[] path = new string[] { _env.WebRootPath, "images", "upload", Image.FileName };
                var saveImage = Path.Combine(path);


                if (imageExtension == ".jpg" || imageExtension == ".jpeg" || imageExtension == ".png" || imageExtension == ".png" || imageExtension == ".png")
                {
                    using (var uploadImage = new FileStream(saveImage, FileMode.Create))
                    {
                        await Image.CopyToAsync(uploadImage);
                    }
                }
                else
                {
                    //add viewdata for everything
                    ViewData["DANGER"] = "Please have the extension be .jpg .jpeg .png .gif or bmp";
                    return View("CreateCustomer");
                }

                //add it to the database
                image.InvoiceId = invoiceId;
                image.Image = Image.FileName;
                _context.Add(image);
                await _context.SaveChangesAsync();

                TempData["name"] = name;
                TempData["customerId"] = customerId;
                TempData["HST"] = HST;
                TempData["invoiceId"] = invoiceId;
                return RedirectToAction(nameof(CustomerIndex));


            }
            catch (Exception)
            {
            }

            return View();
        }

        // GET: InvoiceImages/EditCustomer/5
        public async Task<IActionResult> EditCustomer(int? id, int invoiceId, string? name, int? customerId, int? HST)
        {
            try
            {
                ViewData["invoiceId"] = invoiceId;
                ViewData["name"] = name;
                ViewData["imageId"] = id;
                ViewData["customerId"] = customerId;
                ViewData["HST"] = HST;
            }
            catch (Exception)
            {

            }

            if (id == null)
            {
                return NotFound();
            }

            var invoiceImage = await _context.InvoiceImages.FindAsync(id);
            if (invoiceImage == null)
            {
                return NotFound();
            }
            return View(invoiceImage);
        }

        // POST: InvoiceImages/EditCustomer/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(int id, [Bind("ImageId,InvoiceId,Image")] InvoiceImage invoiceImage, IFormFile Image, int invoiceId, string name, int? customerId, int? HST)
        {
            /*if (id != invoiceImage.ImageId)
            {
                return NotFound();
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    invoiceImage.ImageId = id;
                    invoiceImage.Image = Image.FileName;
                    _context.Update(invoiceImage);
                    await _context.SaveChangesAsync();

                    string imageExtension = Path.GetExtension(Image.FileName);

                    string[] path = new string[] { _env.WebRootPath, "images", "upload", Image.FileName };
                    var saveImage = Path.Combine(path);


                    if (imageExtension == ".jpg" || imageExtension == ".jpeg" || imageExtension == ".png" || imageExtension == ".gif" || imageExtension == ".bmp")
                    {
                        using (var uploadImage = new FileStream(saveImage, FileMode.Create))
                        {
                            await Image.CopyToAsync(uploadImage);
                        }
                    }
                    else
                    {
                        //add viewdata for everything
                        ViewData["DANGER"] = "Please have the extension be .jpg .jpeg .png .gif or .bmp";
                        return View();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceImageExists(invoiceImage.ImageId))
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
                TempData["invoiceId"] = invoiceId;
                return RedirectToAction(nameof(CustomerIndex));
            }
            return View(invoiceImage);
        }

        // GET: InvoiceImages/Edit/5
        public async Task<IActionResult> Edit(int? id, int invoiceId, string? name, int? subcontractorId, int? HST)
        {
            try
            {
                ViewData["invoiceId"] = invoiceId;
                ViewData["name"] = name;
                ViewData["imageId"] = id;
                ViewData["subcontractorId"] = subcontractorId;
                ViewData["HST"] = HST;
            }
            catch(Exception)
            {

            }

            if (id == null)
            {
                return NotFound();
            }

            var invoiceImage = await _context.InvoiceImages.FindAsync(id);
            if (invoiceImage == null)
            {
                return NotFound();
            }
            return View(invoiceImage);
        }

        // POST: InvoiceImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageId,InvoiceId,Image")] InvoiceImage invoiceImage, IFormFile Image, int invoiceId, string name, int? subcontractorId, int? HST)
        {
            /*if (id != invoiceImage.ImageId)
            {
                return NotFound();
            }*/

            if (ModelState.IsValid)
            {
                try
                {
                    invoiceImage.ImageId = id;
                    invoiceImage.Image = Image.FileName;
                    _context.Update(invoiceImage);
                    await _context.SaveChangesAsync();

                    string imageExtension = Path.GetExtension(Image.FileName);

                    string[] path = new string[] { _env.WebRootPath, "images", "upload", Image.FileName };
                    var saveImage = Path.Combine(path);


                    if (imageExtension == ".jpg" || imageExtension == ".jpeg" || imageExtension == ".png" || imageExtension == ".gif" || imageExtension == ".bmp")
                    {
                        using (var uploadImage = new FileStream(saveImage, FileMode.Create))
                        {
                            await Image.CopyToAsync(uploadImage);
                        }
                    }
                    else
                    {
                        //add viewdata for everything
                        ViewData["DANGER"] = "Please have the extension be .jpg .jpeg .png .gif or .bmp";
                        return View();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceImageExists(invoiceImage.ImageId))
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
                TempData["invoiceId"] = invoiceId;
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceImage);
        }

        // GET: InvoiceImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceImage = await _context.InvoiceImages
                .FirstOrDefaultAsync(m => m.ImageId == id);
            if (invoiceImage == null)
            {
                return NotFound();
            }

            return View(invoiceImage);
        }

        // POST: InvoiceImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceImage = await _context.InvoiceImages.FindAsync(id);
            _context.InvoiceImages.Remove(invoiceImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceImageExists(int id)
        {
            return _context.InvoiceImages.Any(e => e.ImageId == id);
        }
    }
}

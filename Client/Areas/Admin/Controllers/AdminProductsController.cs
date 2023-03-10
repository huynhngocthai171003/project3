using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client.Models;
/*using Client.Helpper;*/
using PagedList.Core;
using Grpc.Core;
using Microsoft.Extensions.Hosting;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminProductsController : Controller
    {
        private readonly ePRJContext _context;
        private IWebHostEnvironment _environment;

        public AdminProductsController(ePRJContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Admin/AdminProducts
        public IActionResult Index(int page = 1, int CatID = 0)
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {
                var pageNumber = page;
                var pageSize = 8;

                List<Product> IsProducts = new List<Product>();
                if (CatID != 0)
                {
                    IsProducts = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CategoryId == CatID)
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.Id).ToList();
                }
                else
                {
                    IsProducts = _context.Products
                    .AsNoTracking()
                    .Include(x => x.Category)
                    .OrderByDescending(x => x.Id).ToList();
                }

                PagedList<Product> models = new PagedList<Product>(IsProducts.AsQueryable(), pageNumber, pageSize);
                ViewBag.CurrentCateID = CatID;
                ViewBag.CurrentPage = pageNumber;
                ViewData["Categories"] = new SelectList(_context.Categorys, "Id", "Name");
                return View(models);
            }
            return RedirectToAction("Index", "AdminLogin");
        }

        public IActionResult Filter(int CatID = 0)
        {
            var url = $"/Admin/AdminProducts?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/Admin/AdminProducts";
            }
            return Json(new { status = "success", redirectUrl = url });
        }

        // GET: Admin/AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["Categories"] = new SelectList(_context.Categorys, "Id", "Name");
            return View();
        }

        // POST: Admin/AdminProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult?> Create(ProductDto product)
        {
            if (ModelState.IsValid)
            {


                string filename = string.Empty;
                string filename1 = string.Empty;
                string filename2 = string.Empty;
                string filename3 = string.Empty;
                if (product.Avatar != null)
                {
                    filename = product.Avatar.FileName;
                    var imageFolder = Path.Combine(_environment.WebRootPath + "/images");

                    if (!Directory.Exists(imageFolder))
                    {
                        Directory.CreateDirectory(imageFolder);
                    }

                    var filePath = Path.Combine(imageFolder, filename);
                    var stream = new FileStream(filePath, FileMode.Create);
                    await product.Avatar.CopyToAsync(stream);
                }
                if (product.Image1 != null)
                {
                    filename1 = product.Image1.FileName;
                    var imageFolder1 = Path.Combine(_environment.WebRootPath + "/images");

                    if (!Directory.Exists(imageFolder1))
                    {
                        Directory.CreateDirectory(imageFolder1);
                    }

                    var filePath = Path.Combine(imageFolder1, filename1);
                    var stream = new FileStream(filePath, FileMode.Create);
                    await product.Image1.CopyToAsync(stream);
                }
                if (product.Image2 != null)
                {
                    filename2 = product.Image2.FileName;
                    var imageFolder2 = Path.Combine(_environment.WebRootPath + "/images");

                    if (!Directory.Exists(imageFolder2))
                    {
                        Directory.CreateDirectory(imageFolder2);
                    }

                    var filePath = Path.Combine(imageFolder2, filename2);
                    var stream = new FileStream(filePath, FileMode.Create);
                    await product.Image2.CopyToAsync(stream);
                }
                if (product.Image3 != null)
                {
                    filename3 = product.Image3.FileName;
                    var imageFolder3 = Path.Combine(_environment.WebRootPath + "/images");

                    if (!Directory.Exists(imageFolder3))
                    {
                        Directory.CreateDirectory(imageFolder3);
                    }

                    var filePath = Path.Combine(imageFolder3, filename3);
                    var stream = new FileStream(filePath, FileMode.Create);
                    await product.Image3.CopyToAsync(stream);
                }
                var i = new Product { ProductName = product.ProductName, Rate = product.Rate, Price = product.Price, Description = product.Description, Avatar = filename, Image1 = filename1, Image2 = filename2, Image3 = filename3, CategoryId = product.CategoryId, Status = product.Status };
                _context.Products.Add(i);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categories"] = new SelectList(_context.Categorys, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();

            }


            Product? product = await _context.Products!.FirstOrDefaultAsync(x => x.Id == id!);

            if (product == null)
            {
                return NotFound();
            }

            ProductDto dto = new ProductDto { ProductName = product.ProductName, Rate = product.Rate, Price = product.Price, Description = product.Description, CategoryId = product.CategoryId, Status = product.Status };
            ViewData["Categories"] = new SelectList(_context.Categorys, "Id", "Name", product.CategoryId);
            return View(dto);


        }

        // POST: Admin/AdminProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDto product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Product? old = await _context.Products!.FirstOrDefaultAsync(x => x.Id == id);
                try
                {
                    string filename = string.Empty;
                    string filename1 = string.Empty;
                    string filename2 = string.Empty;
                    string filename3 = string.Empty;
                    if (product.Avatar != null)
                    {
                        filename = product.Avatar.FileName;
                        var imageFolder = Path.Combine(_environment.WebRootPath + "/images");

                        if (!Directory.Exists(imageFolder))
                        {
                            Directory.CreateDirectory(imageFolder);
                        }

                        var filePath = Path.Combine(imageFolder, filename);
                        var stream = new FileStream(filePath, FileMode.Create);
                        await product.Avatar.CopyToAsync(stream);
                    }
                    if (product.Image1 != null)
                    {
                        filename1 = product.Image1.FileName;
                        var imageFolder1 = Path.Combine(_environment.WebRootPath + "/images");

                        if (!Directory.Exists(imageFolder1))
                        {
                            Directory.CreateDirectory(imageFolder1);
                        }

                        var filePath = Path.Combine(imageFolder1, filename1);
                        var stream = new FileStream(filePath, FileMode.Create);
                        await product.Image1.CopyToAsync(stream);
                    }
                    if (product.Image2 != null)
                    {
                        filename2 = product.Image2.FileName;
                        var imageFolder2 = Path.Combine(_environment.WebRootPath + "/images");

                        if (!Directory.Exists(imageFolder2))
                        {
                            Directory.CreateDirectory(imageFolder2);
                        }

                        var filePath = Path.Combine(imageFolder2, filename2);
                        var stream = new FileStream(filePath, FileMode.Create);
                        await product.Image2.CopyToAsync(stream);
                    }
                    if (product.Image3 != null)
                    {
                        filename3 = product.Image3.FileName;
                        var imageFolder3 = Path.Combine(_environment.WebRootPath + "/images");

                        if (!Directory.Exists(imageFolder3))
                        {
                            Directory.CreateDirectory(imageFolder3);
                        }

                        var filePath = Path.Combine(imageFolder3, filename3);
                        var stream = new FileStream(filePath, FileMode.Create);
                        await product.Image3.CopyToAsync(stream);
                    }

                    /*                    var i = new Product { ProductName = product.ProductName, Rate = product.Rate, Price = product.Price, Description = product.Description, Avatar = filename, Image1 = filename1, Image2 = filename2, Image3 = filename3, CategoryId = product.CategoryId, Status = product.Status };
                     *                    
                    */
                    old.ProductName = product.ProductName;
                    old.Rate = product.Rate;
                    old.Price = product.Price;
                    old.ProductName = product.ProductName;
                    old.Description = product.Description;
                    old.Avatar = filename;
                    old.Image1 = filename1;
                    old.Image2 = filename2;
                    old.Image3 = filename3;
                    old.CategoryId = product.CategoryId;
                    old.Status = product.Status;

                    _context.Products.Update(old);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["Categories"] = new SelectList(_context.Categorys, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Admin/AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ePRJContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

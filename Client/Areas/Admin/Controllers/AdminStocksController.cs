using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Client.Models;
using PagedList.Core;
using System.Drawing.Printing;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminStocksController : Controller
    {
        private readonly ePRJContext _context;
        private IWebHostEnvironment _environment;

        public AdminStocksController(ePRJContext context, IWebHostEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }

        // GET: Admin/AdminStocks
        public IActionResult IndexStock(int page = 1, int CatID = 0)
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


                /*var q = _context.Stocks.AsNoTracking().Where(x => x.ProductId == ).Include(x => x.Product);*/


                PagedList<Product> models = new PagedList<Product>(IsProducts.AsQueryable(), pageNumber, pageSize);
                ViewBag.CurrentCateID = CatID;
                ViewBag.CurrentPage = pageNumber;
                ViewData["Categories1"] = new SelectList(_context.Categorys, "Id", "Name");
                return View(models);
            }
            return RedirectToAction("Index", "AdminLogin");
        }

        public IActionResult Filter(int CatID = 0)
        {
            var url = $"/Admin/AdminStocks/IndexStock?CatID={CatID}";
            if (CatID == 0)
            {
                url = $"/Admin/AdminStocks/IndexStock";
            }
            return Json(new { status = "success", redirectUrl = url });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();

            }


            Stock? stock = await _context.Stocks!.FirstOrDefaultAsync(x => x.ProductId == id!);

            /*if (stock == null)
            {
                return NotFound();
            }*/


            Stock dto = new Stock { ProductId = stock.ProductId, CategoryId = stock.CategoryId, Quantity = stock.Quantity };
            /*ViewBag.Product = _context.Products.Where(x => x.Id == id);
            ViewBag.Category = _context.Products.Where(x => x.);*/
            return View(dto);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Stock stock)
        {
            if (id != stock.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Stock? old = await _context.Stocks!.FirstOrDefaultAsync(x => x.ProductId == id);

                var YourId = Convert.ToInt16(Request.Form["Id"]);

                old.ProductId = stock.ProductId;
                var YourCat = Convert.ToInt16(Request.Form["CategoryId"]);
                old.CategoryId = stock.CategoryId;
                var YourQuantity = Convert.ToInt16(Request.Form["Quantity"]);

                old.Quantity = stock.Quantity;

                _context.Stocks.Update(old);
                await _context.SaveChangesAsync();

                return RedirectToAction("Edit");
            }
            /*ViewData["Categories"] = new SelectList(_context.Categorys, "Id", "Name", product.CategoryId);*/
            /*return View(stock);*/
            return RedirectToAction("IndexStock");
        }

        private bool StockExists(int productId)
        {
            throw new NotImplementedException();
        }
    }
}

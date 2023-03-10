using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace Client.Controllers
{
    public class SearchController : Controller
    {
        private readonly ePRJContext _context;

        public SearchController(ePRJContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult FindProduct(string keyword, int? page, int Id)
        {
            try
            {
                var recentPost = _context.Comments.AsNoTracking().Include(x => x.Customer).Include(x => x.Product).Take(3).OrderByDescending(x => x.Id).ToList();

                /*                var danhmuc1 = _context.Categorys.AsNoTracking().SingleOrDefault(x => x.Id == Id);*/
                var danhmuc = _context.Categorys.ToList();
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 9;
                var IsProduct = _context.Products
                    .AsNoTracking()
                    .Where(x => x.ProductName.Contains(keyword))
                    .OrderByDescending(x => x.Id);
                PagedList<Product> models = new PagedList<Product>(IsProduct, pageNumber, pageSize);

                ViewBag.Category = danhmuc;
                ViewBag.Post = recentPost;

                /*                ViewBag.CurrentCat = danhmuc1;*/
                ViewBag.CurrentPage = pageNumber;
                return View(models);



            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System.Diagnostics;
using System.Drawing.Printing;
/*using System.Web.Mvc;*/

namespace Client.Controllers
{
    public class HomeController : Controller
    {

        private readonly ePRJContext _context;
        private readonly ILogger<HomeController> _logger;

        
        public HomeController(ILogger<HomeController> logger, ePRJContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var IsProduct = _context.Products
                    .AsNoTracking()
                    .OrderByDescending(x => x.Id)
                    .Take(4);

            var lsPopular = _context.Products.AsNoTracking().OrderByDescending(x => x.Status == true).Take(4);
            var danhmuc = _context.Categorys.ToList();
            ViewBag.Category = danhmuc;
            ViewBag.Popular = lsPopular;
            List<Product> models = new List<Product>(IsProduct);
            /*                ViewBag.CurrentCat = danhmuc1;*/
   
            return View(models);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*[ChildActionOnly]*/
       /* public IActionResult _Header()
        {
            var model = db.Categorys.ToList();
*//*            ViewBag.cat = model;
*//*            return View(model);
            *//*return PartialView("_Header", db.Categorys.ToList());*/
            /*return View("_Header", model);*//*
        }*/

        public PartialViewResult Category()
        {
            return PartialView("_Header");
        }
    }
}
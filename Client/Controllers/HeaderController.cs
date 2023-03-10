using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace Client.Controllers
{
    public class HeaderController : Controller
    {
        private readonly ePRJContext _context;
        public HeaderController(ePRJContext context)
        {
            _context = context;
        }
        public IActionResult _Header1()
        {
            List<Category> c = _context.Categorys.ToList();
            /*List<Category> IsCategory = new List<Category>();
            var danhmuc1 = _context.Categorys.ToList();*/
            return View(c);
            /*ViewBag.Category = danhmuc1;
            return View();*/
            

        }
        
    }
}

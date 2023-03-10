using Microsoft.AspNetCore.Mvc;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult IndexDashboard()
        {
            if (HttpContext.Session.GetInt32("IdAdmin") != null)
            {

                return View();
            }
            return RedirectToAction("Index", "AdminLogin");
        }
    }
}

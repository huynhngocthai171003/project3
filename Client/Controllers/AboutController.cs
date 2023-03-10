using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult IndexAbout()
        {
            return View();
        }
    }
}

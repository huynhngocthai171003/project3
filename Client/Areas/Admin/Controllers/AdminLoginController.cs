using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoginController : Controller
    {
        ePRJContext _context;
        public AdminLoginController(ePRJContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Staffs!.SingleOrDefaultAsync(u => u.UserName == username && u.PassWord == password);
                var u = username;
                if (user != null)
                {
                    var session = HttpContext.Session;
                    session.SetInt32("IdAdmin", user.Id);
                    session.SetString("UsernameAdmin", user.UserName);
                    session.SetString("FullNameAdmin", user.Name);
                    session.SetInt32("Role", user.Role);
                    TempData["message_login_success"] = "Login successful!";
                    return RedirectToAction("IndexDashboard", "Home");
                }

            }
            TempData["message_login"] = "Account not found!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
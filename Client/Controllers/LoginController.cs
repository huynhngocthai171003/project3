using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Client.Controllers
{
    public class LoginController : Controller
    {
        ePRJContext _context;
        public LoginController(ePRJContext context)
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
                var user = await _context.Customers!.SingleOrDefaultAsync(u => u.UserName == username && u.PassWord == password);
                var u = username;
                if (user != null)
                {
                    var checkActive = await _context.Customers!.SingleOrDefaultAsync(u => u.UserName == username && u.PassWord == password && u.Active == 1);
                    if (checkActive != null)
                    {
                        var session = HttpContext.Session;
                        session.SetInt32("CustomerId", user.Id);
                        session.SetString("Username", user.UserName);
                        session.SetString("FullName", user.FullName);
                        session.SetString("Email", user.Email);
                        session.SetString("PhoneNumber", user.PhoneNumber);
                        session.SetString("Address", user.UserAddress);
                        TempData["message_login_success"] = "Login successful!";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["message_login"] = "Your account is locked!";
                        return RedirectToAction("Index");
                    }
                }

            }
            TempData["message_login"] = "Account not found!";
            return RedirectToAction("Index");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Customer data)
        {
            var email = await _context.Customers!.SingleOrDefaultAsync(u => u.Email.Equals(data.Email));
            if (email != null)
            {
                TempData["message_register"] = "Email already exists!";
                return RedirectToAction("Register");
            }
            var tel = await _context.Customers!.SingleOrDefaultAsync(u => u.PhoneNumber.Equals(data.PhoneNumber));
            if (tel != null)
            {
                TempData["message_register"] = "Contact number already exists!";
                return RedirectToAction("Register");
            }
            var username = await _context.Customers!.SingleOrDefaultAsync(u => u.UserName.Equals(data.UserName));
            if (username != null)
            {
                TempData["message_register"] = "Username already exists!";
                return RedirectToAction("Register");
            }
            if (ModelState.IsValid)
            {
                data.PassWord = data.PassWord;
                data.Active = 1;
                _context.Customers!.Add(data);
                await _context.SaveChangesAsync();


                await _context.SaveChangesAsync();

                TempData["message2"] = "Registration successful!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

    }
}

using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult IndexContact()
		{
			return View();
		}
	}
}

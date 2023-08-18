using AdminPage.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminPage.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private UserContext db;
		public HomeController(ILogger<HomeController> logger, UserContext context)
		{
			_logger = logger;
			db = context;
		}

		[Authorize]
		public IActionResult Index()
		{
			var users = db.Users.ToList();
			return View("UsersView", users);
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
        public IActionResult BlockUser(int userId)
        {
            var user = db.Users.Find(userId);
            if (user != null)
            {
                user.Status = "Blocked";
                db.SaveChanges();
            }
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult UnblockUser(int userId)
        {
            var user = db.Users.Find(userId);
            if (user != null)
            {
                user.Status = "Active";
                db.SaveChanges();
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult DeleteUser(int userId)
        {
            var user = db.Users.Find(userId);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}

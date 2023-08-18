using AdminPage.Models;
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
			UsersViewModel users = new UsersViewModel();
			users.Users = db.Users.ToList();
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
	}
}
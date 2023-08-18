using AdminPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace AdminPage.Controllers
{
	public class AuthController : Controller
	{
		private UserContext db;
		public AuthController(UserContext context)
		{
			db = context;
		}

		[HttpGet]
		public IActionResult login()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				model.Email = model.Email.ToLower();
				User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
				if (user != null)
				{
					await Authenticate(model.Email);

					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Incorrect password or Email");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterModel model)
		{
			db.Users.Where(u => u.Email != null).ExecuteDelete();  //erasing database 
			if (ModelState.IsValid)
			{

				model.Email = model.Email.ToLower();
				User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
				if (user == null)
				{
					db.Users.Add(new User { Email = model.Email, Password = model.Password, Online = true, CreatedDate = DateTime.Now, Name = model.Name });
					await db.SaveChangesAsync();

					await Authenticate(model.Email);

					return RedirectToAction("Index", "Home");
				}
				else
					ModelState.AddModelError("", "Incorrect password or Email");
			}

			return View(model);
		}
		private async Task Authenticate(string userName)
		{
			var user = db.Users.FirstOrDefault(u => u.Email == userName);

			user.LastLogin = DateTime.Now;

			db.Users.Update(user);
			var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
			};
			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}
		[Authorize]
		public async Task<IActionResult> Logout()
		{

			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Auth");
		}
	}

}


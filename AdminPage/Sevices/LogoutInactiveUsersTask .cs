using AdminPage.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AdminPage.Sevices
{																								//not working
	public class LogoutInactiveUsersTask /*: IHostedService, IDisposable*/
	{
		private readonly IServiceProvider _serviceProvider;
		private Timer _timer;

		public LogoutInactiveUsersTask(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		//public Task StartAsync(CancellationToken cancellationToken)
		//{
		//	_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(30)); // Периодическая задача каждые 30 минут
		//	return Task.CompletedTask;
		//}

		//private async void DoWork(object state)
		//{
		//	using (var scope = _serviceProvider.CreateScope())
		//	{
		//		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
		//		var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<User>>();

		//		var usersToLogout = userManager.Users.Where(u => u.LastActivity < DateTime.UtcNow.AddMinutes(-1)).ToList();

		//		foreach (var user in usersToLogout)
		//		{
		//			var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
		//			var context = httpContextAccessor.HttpContext;
		//			var claims = new List<Claim>
		//			{
		//				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
		//			};
		//			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
		//			context.User =new ClaimsPrincipal(id);
		//			await signInManager.SignOutAsync();

		//		}
		//	}
		//}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			_timer?.Change(Timeout.Infinite, 0);
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}
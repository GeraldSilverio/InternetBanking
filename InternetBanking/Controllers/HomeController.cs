using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUserServices _userServices;

        public HomeController(IUserServices userServices)
        {
            
            _userServices = userServices;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userServices.GetAllAsync();
            var homeView = new HomeView()
            {
                ActiveUser = users.Where(x => x.IsActive = true).Count(),
                InActiveUser = users.Where(x => x.IsActive = false).Count(),
            };
            return View(homeView);
        }

        [Authorize(Roles = "Client")]
        public IActionResult IndexClient()
        {
            return View();
        }
    }
}

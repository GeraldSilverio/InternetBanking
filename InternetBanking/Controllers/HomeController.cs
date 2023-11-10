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
        public IActionResult Index()
        {
            var users = _userServices.GetAllAsync();
            var homeView = new HomeView();
            homeView.ActiveUser = users.Where(x => x.IsActive == true).Count();
            homeView.InActiveUser = users.Where(x => x.IsActive == false).Count();
            return View(homeView);
        }

        [Authorize(Roles = "Client")]
        public IActionResult IndexClient()
        {
            return View();
        }
    }
}

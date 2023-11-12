using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{

    public class ClientController : Controller
    {
        private readonly IUserServices _userServices;

        public ClientController(IUserServices userServices)
        {
            
            _userServices = userServices;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

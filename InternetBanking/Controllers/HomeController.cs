using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.InternetBanking.Middlewares;

namespace WebApp.InternetBanking.Controllers
{
    
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Client")]
        public IActionResult IndexClient()
        {
            return View();
        }
    }
}

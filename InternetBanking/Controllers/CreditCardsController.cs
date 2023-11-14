using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{
    public class CreditCardsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

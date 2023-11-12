using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult SavingAccount()
        {
            return View();
        }
    }
}

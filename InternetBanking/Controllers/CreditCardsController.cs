using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.CreditCards;
using InternetBanking.Infraestructure.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{
    public class CreditCardsController : Controller
    {
        private readonly ICreditCardsService _creditCardsService;
        private readonly IAccountService _accountService;

        public CreditCardsController(ICreditCardsService creditCardsService, IAccountService accountService)
        {
            _creditCardsService = creditCardsService;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult NewCreditCard()
        {
            ViewBag.Users = _accountService.GetAllUsersAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewCreditCard(SaveCardViewModel model)
        {
            

            await _creditCardsService.Add(model);
            return RedirectToRoute(new {controller = "CreditCards",action ="Index"});
        }
    }
}

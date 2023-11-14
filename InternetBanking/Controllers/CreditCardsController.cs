using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Services;
using InternetBanking.Core.Application.ViewModels.CreditCards;
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

        public async Task<IActionResult> Index()
        {
            return View(await _creditCardsService.GetAll());
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

        public async Task<IActionResult> DeleteCard(int id)
        {
            var card = await _creditCardsService.GetById(id);
            return View(card);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCardPost(int id)
        {
            try
            {
                var card = await _creditCardsService.GetById(id);
                await _creditCardsService.Delete(id);
                return RedirectToRoute(new { controller = "CreditCards", action = "Index" });

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}

using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.CreditCards;
using InternetBanking.Core.Application.ViewModels.Filter;
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

        public async Task<IActionResult> Index(FilterIdentityCardViewModel fvm)
        {
            return View(await _creditCardsService.GetAllWithFilters(fvm));
        }
        public IActionResult NewCreditCard()
        {
            ViewBag.Users = _accountService.GetAllUsersAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewCreditCard(string userId,string selectCard)
        {
            try
            {
                var model = new SaveCardViewModel()
                {
                    IdUser = userId,
                    SelectCard = selectCard,
                };
                await _creditCardsService.Add(model);
                return RedirectToRoute(new { controller = "CreditCards", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

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

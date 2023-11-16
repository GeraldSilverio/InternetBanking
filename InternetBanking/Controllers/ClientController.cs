using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{

    public class ClientController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly ISavingAccountService _savingAccountService;
        private readonly ICreditCardsService _cardsService;
        private readonly IMoneyLoanService _moneyLoanService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ClientController(IUserServices userServices
            , ISavingAccountService savingAccountService
            , ICreditCardsService cardsService
            , IMoneyLoanService moneyLoanService
            , IHttpContextAccessor contextAccessor)
        {

            _userServices = userServices;
            _savingAccountService = savingAccountService;
            _cardsService = cardsService;
            _moneyLoanService = moneyLoanService;
            _contextAccessor = contextAccessor;

        }

        public async Task<IActionResult> Index()
        {
            var User = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            ViewBag.SavingAccounts = await _savingAccountService.GetAccountsByUserId(User.Id);
            ViewBag.CreditCards = await _cardsService.GetCreditCardsById(User.Id);
            ViewBag.MoneyLoans = await _moneyLoanService.GetMoneyLoansByUserId(User.Id);
            return View();
        }
    }
}

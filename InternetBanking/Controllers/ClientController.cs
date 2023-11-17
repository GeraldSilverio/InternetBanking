using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Services;
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
        private readonly AuthenticationResponse user;

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
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        }

        public async Task<IActionResult> Index()
        {

            ViewBag.SavingAccounts = await _savingAccountService.GetAccountsByUserId(user.Id);
            ViewBag.CreditCards = await _cardsService.GetCreditCardsById(user.Id);
            ViewBag.MoneyLoans = await _moneyLoanService.GetMoneyLoansByUserId(user.Id);
            return View();
        }
    }
}

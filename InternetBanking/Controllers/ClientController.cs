using InternetBanking.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{

    public class ClientController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly ISavingAccountService _accountService;
        private readonly ICreditCardsService _cardsService;
        private readonly IMoneyLoanService _moneyLoanService;

        public ClientController(IUserServices userServices
            , ISavingAccountService accountService
            , ICreditCardsService cardsService
            , IMoneyLoanService moneyLoanService)
        {
            
            _userServices = userServices;
            _accountService = accountService;
            _cardsService = cardsService;
            _moneyLoanService = moneyLoanService;
            
        }

        public async Task<IActionResult> Index()
        {

            ViewBag.SavingAccounts = await _accountService.GetAll();
            ViewBag.CreditCards = await _cardsService.GetAll();
            ViewBag.MoneyLoans = await _moneyLoanService.GetAll();
            return View();
        }
    }
}

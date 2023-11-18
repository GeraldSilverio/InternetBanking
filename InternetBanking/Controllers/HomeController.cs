using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IGetCountProduct _getCount;
        private readonly IPaymentService _paymentService;
        private readonly ITransactionService _transactionService;

        public HomeController(IUserServices userServices, IGetCountProduct getCount, IPaymentService paymentService, ITransactionService transactionService)
        {

            _userServices = userServices;
            _getCount = getCount;
            _paymentService = paymentService;
            _transactionService = transactionService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var users = _userServices.GetAllAsync();
            var homeView = new HomeView()
            {
                ActiveUser = users.Where(x => x.IsActive == true).Count(),
                InActiveUser = users.Where(x => x.IsActive == false).Count(),
                Products = _getCount.GetCount(),
                PaymentsPerDay = _paymentService.GetPaymentPerDay(),
                Payments = _paymentService.GetCountPayment(),
                TransactionsPerDay = _transactionService.GetTransactionPerDay(),
                Transactions = _transactionService.GetCountTransaction()
            };
            return View(homeView);
        }


    }
}

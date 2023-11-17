using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.ViewModels.Payment.Transaction;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.InternetBanking.Controllers
{
    [Authorize(Roles ="Client")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ISavingAccountService _savingAccountService;
        IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse user;
        public TransactionController(IHttpContextAccessor httpContextAccessor,ISavingAccountService savingAccountService, ITransactionService transactionService)
        {
            _transactionService = transactionService;
            _httpContextAccessor = httpContextAccessor;
            _savingAccountService = savingAccountService;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Account = await _savingAccountService.GetAccountsByUserId(user.Id);
                return View("AddTransaction");

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(SaveTransactionViewModel viewModel)
        {
            try
            {
                viewModel.IdUser = user.Id;
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index");
                }

                var transaction = await _transactionService.AddTransaction(viewModel);
                if (transaction.HasError)
                {
                    transaction.HasError = viewModel.HasError;
                    transaction.Error = viewModel.Error;
                    ViewBag.Account = await _savingAccountService.GetAccountsByUserId(user.Id);

                    return View("AddTransaction", transaction);
                }
                return RedirectToRoute(new { controller = "Client", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}

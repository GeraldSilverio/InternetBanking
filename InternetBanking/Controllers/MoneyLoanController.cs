using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Filter;
using InternetBanking.Core.Application.ViewModels.MoneyLoan;
using InternetBanking.Core.Application.ViewModels.SavingAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{
    [Authorize(Roles ="Admin")]
    public class MoneyLoanController : Controller
    {
        private readonly IMoneyLoanService _moneyLoanService;
        private readonly IAccountService _accountService;

        public MoneyLoanController(IMoneyLoanService moneyLoanService, IAccountService accountService)
        {
            _moneyLoanService = moneyLoanService;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index(FilterIdentityCardViewModel fvm)
        {
            return View(await _moneyLoanService.GetAllWithFilters(fvm));
        }

        [HttpPost]
        public async Task<IActionResult> NewMoneyLoan(string userId,decimal borrowedBalance)
        {
            try
            {
                if (borrowedBalance == decimal.Zero)
                {
                    borrowedBalance = 0.01m;
                }

                var model = new NewMoneyLoanViewModel()
                {
                    IdUser = userId,
                    BorrowedBalance = borrowedBalance,
                };               

                await _moneyLoanService.Add(model);
                return RedirectToRoute(new { controller = "MoneyLoan", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteMoneyLoan(int id)
        {
            var card = await _moneyLoanService.GetById(id);
            return View(card);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMoneyLoanPost(int id)
        {
            try
            {
                var card = await _moneyLoanService.GetById(id);
                await _moneyLoanService.Delete(id);
                return RedirectToRoute(new { controller = "MoneyLoan", action = "Index" });

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}

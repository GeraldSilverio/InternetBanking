using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Filter;
using InternetBanking.Core.Application.ViewModels.SavingAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SavingAccountController : Controller
    {
        private readonly ISavingAccountService _savingAccountService;
        private readonly IAccountService _accountService;

        public SavingAccountController(ISavingAccountService savingAccountService, IAccountService accountService)
        {
            _savingAccountService = savingAccountService;
            _accountService = accountService;
        }

        public async Task<IActionResult> SavingAccount(FilterIdentityCardViewModel fVm)
        {
            var accounts = await _savingAccountService.GetAllWithFilters(fVm);
            return View(accounts);
        }

        public IActionResult NewAccount()
        {
            ViewBag.Users = _accountService.GetAllUsersAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewAccount(string userId, decimal balance)
        {
            try
            {

                var model = new CreateSavingAccountViewModel()
                {
                    IdUser = userId,
                    Balance = balance,
                };

                await _savingAccountService.Add(model);
                return RedirectToRoute(new { controller = "SavingAccount", action = "SavingAccount" });
            }catch(Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult>DeleteAccount(int id)
        {
            var account = await _savingAccountService.GetById(id);
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccountPost(int id)
        {
            try
            {
                var account = await _savingAccountService.GetById(id);
                await _savingAccountService.Delete(id);
                await _savingAccountService.UpdatePrincialAccount(account.Balance, account.IdUser);
                return RedirectToRoute(new { controller = "SavingAccount", action = "SavingAccount" });

            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}

using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.SavingAccount;
using InternetBanking.Infraestructure.Persistence.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ISavingAccountService _savingAccountService;
        private readonly IAccountService _accountService;

        public ProductsController(ISavingAccountService savingAccountService, IAccountService accountService)
        {
            _savingAccountService = savingAccountService;
            _accountService = accountService;
        }

        public async Task<IActionResult> SavingAccount()
        {
            var accounts = await _savingAccountService.GetAll();
            return View(accounts);
        }

        public IActionResult NewAccount()
        {
            ViewBag.Users = _accountService.GetAllUsersAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewAccount(CreateSavingAccountViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Users = _accountService.GetAllUsersAsync();
                    return View(model);
                }

                await _savingAccountService.Add(model);
                return RedirectToRoute(new { controller = "Products", action = "SavingAccount" });


            }catch(Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}

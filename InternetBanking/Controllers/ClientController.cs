using InternetBanking.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{

    public class ClientController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly ISavingAccountService _accountService;

        public ClientController(IUserServices userServices, ISavingAccountService accountService)
        {
            
            _userServices = userServices;
            _accountService = accountService;
            
        }

        public async Task<IActionResult> Index()
        {

            ViewBag.SavingAccounts = await _accountService.GetAll();
            return View();
        }
    }
}

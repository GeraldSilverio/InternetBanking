using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Beneficiary;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{
    public class BeneficiaryController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IAccountService _accountService;

        public BeneficiaryController(IBeneficiaryService beneficiaryService, IHttpContextAccessor httpContextAccessor, IAccountService accountService)
        {
            _httpContextAccessor = httpContextAccessor;
            _beneficiaryService = beneficiaryService;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            var User = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var Beneficiary = await _beneficiaryService.GetAllByUser(User.Id);

            ViewBag.Beneficiaries = await _accountService.GetAllClients();

            List<BeneficiaryViewModel>  beneficiaryViewModel = Beneficiary.ToList();
            return View(beneficiaryViewModel);
        }
    }
}

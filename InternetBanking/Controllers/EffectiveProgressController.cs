using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using InternetBanking.Core.Application.ViewModels.Payment.EffectiveProgress;

namespace WebApp.InternetBanking.Controllers
{
    [Authorize(Roles = "Client")]
    public class EffectiveProgressController : Controller
    {
        private readonly IEffectiveProgressService _effectiveProgressService;
        private readonly ICreditCardsService _creditCardsService;
        private readonly ISavingAccountService _savingAccountService;
        IHttpContextAccessor _httpContextAccessor;
        private AuthenticationResponse? user;
        public EffectiveProgressController(IEffectiveProgressService effectiveProgressService, IHttpContextAccessor httpContextAccessor,
            ICreditCardsService creditCardsService, ISavingAccountService savingAccountService)
        {
            _effectiveProgressService = effectiveProgressService;
            _httpContextAccessor = httpContextAccessor;
            _creditCardsService = creditCardsService;
            _savingAccountService = savingAccountService;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.CreditCard = await _creditCardsService.GetCreditCardsByUserId(user.Id);
                ViewBag.Account = await _savingAccountService.GetAccountsByUserId(user.Id);
                return View("AddEffectiveProgress");

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EffectiveProgress(SaveEffectiveProgressViewModel viewModel)
        {
            try
            {
                viewModel.IdUser = user.Id;
                if (!ModelState.IsValid)
                {

                    ViewBag.CreditCard = await _creditCardsService.GetCreditCardsByUserId(user.Id);
                    ViewBag.Account = await _savingAccountService.GetAccountsByUserId(user.Id);
                    return View("AddEffectiveProgress",viewModel);
                }

                var effectiveProgress = await _effectiveProgressService.AddEffectiveProgress(viewModel);
                if (effectiveProgress.HasError)
                {
                    effectiveProgress.HasError = viewModel.HasError;
                    effectiveProgress.Error = viewModel.Error;
                    ViewBag.CreditCard = await _creditCardsService.GetCreditCardsByUserId(user.Id);
                    ViewBag.Account = await _savingAccountService.GetAccountsByUserId(user.Id);

                    return View("AddEffectiveProgress", effectiveProgress);
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

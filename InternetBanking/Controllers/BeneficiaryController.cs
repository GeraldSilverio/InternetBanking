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
        public BeneficiaryController(IBeneficiaryService beneficiaryService, IHttpContextAccessor httpContextAccessor, IAccountService accountService)
        {
            _httpContextAccessor = httpContextAccessor;
            _beneficiaryService = beneficiaryService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var User = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

                var Beneficiaries = await _beneficiaryService.GetAllByUser(User.Id);

                return View(Beneficiaries);

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }


        }
        #region Create
        public IActionResult AddBeneficiary()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBeneficiary(SaveBeneficiaryViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var beneficiary = await _beneficiaryService.Add(model);
                if (beneficiary.HasError)
                {
                    return View(beneficiary);
                }
                return RedirectToRoute(new { controller = "Beneficiary", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public async Task<IActionResult> DeleteBeneficiary(int id)
        {
            try
            {
                var beneficiary = await _beneficiaryService.GetById(id);
                return View(beneficiary);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteBeneficiaryPost(int id)
        {
            try
            {
                await _beneficiaryService.Delete(id);
                return RedirectToRoute(new { controller = "Beneficiary", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }


        #endregion
    }
}

using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment;
using Microsoft.AspNetCore.Mvc;
using InternetBanking.Core.Application.Helpers;

namespace WebApp.InternetBanking.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IMoneyLoanService _moneyLoanService;
        private readonly AuthenticationResponse? user;
        public PaymentController(IPaymentService paymentService, ISavingAccountService savingAccountService, IHttpContextAccessor contextAccessor, IBeneficiaryService beneficiaryService, IMoneyLoanService moneyLoanService)
        {
            _paymentService = paymentService;
            _savingAccountService = savingAccountService;
            _contextAccessor = contextAccessor;
            _beneficiaryService = beneficiaryService;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _moneyLoanService = moneyLoanService;
        }
        #region ExpressPayment
        public async Task<IActionResult> ExpressPayment()
        {
            ViewBag.Accounts = await _savingAccountService.GetAccountsByUserId(user.Id);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExpressPayment(SavePaymentViewModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    ViewBag.Accounts = await _savingAccountService.GetAccountsByUserId(user.Id);
                    return View(model);

                }

                var payment = await _paymentService.ValidateExpressPayment(model);

                if (payment.HasError)
                {
                    ViewBag.Accounts = await _savingAccountService.GetAccountsByUserId(user.Id);
                    return View(payment);
                }
                return View("ConfirmPayment", payment);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }
        public IActionResult ConfirmPayment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmExpressPayment(SavePaymentViewModel model)
        {
            try
            {
                await _paymentService.Payment(model);
                return RedirectToRoute(new { controller = "Client", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        #endregion

        #region BeneficiaryPayment

        public async Task<IActionResult> Beneficiary()
        {
            try
            {
                ViewBag.Accounts = await _savingAccountService.GetAccountsByUserId(user.Id);
                ViewBag.Beneficiaries = await _beneficiaryService.GetAllByUser(user.Id);
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Beneficiary(SavePaymentViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Accounts = await _savingAccountService.GetAccountsByUserId(user.Id);
                    ViewBag.Beneficiaries = await _beneficiaryService.GetAllByUser(user.Id);
                    return View("Beneficiary", model);
                }
                var payment = await _paymentService.ValidateBeneficiaryPayment(model);
                if (payment.HasError)
                {
                    ViewBag.Accounts = await _savingAccountService.GetAccountsByUserId(user.Id);
                    ViewBag.Beneficiaries = await _beneficiaryService.GetAllByUser(user.Id);
                    return View("Beneficiary", model);
                }
                return View("ConfirmPayment", model);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmBeneficiaryPayment(SavePaymentViewModel model)
        {
            try
            {
                await _paymentService.Payment(model);
                return RedirectToRoute(new { controller = "Client", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }
        #endregion

        #region MoneyLoanPayment
        public async Task<IActionResult> MoneyLoanPayment()
        {
            try
            {
                ViewBag.Accounts = await _savingAccountService.GetAccountsByUserId(user.Id);
                ViewBag.MoneyLoans = await _moneyLoanService.GetMoneyLoansByUserId(user.Id);
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MoneyLoanPayment(SavePaymentViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Accounts = await _savingAccountService.GetAccountsByUserId(user.Id);
                    ViewBag.MoneyLoans = await _moneyLoanService.GetMoneyLoansByUserId(user.Id);
                    return View(model);
                }
                var payment = await _paymentService.MoneyLoanPayment(model);
                if (payment.HasError)
                {
                    ViewBag.Accounts = await _savingAccountService.GetAccountsByUserId(user.Id);
                    ViewBag.MoneyLoans = await _moneyLoanService.GetMoneyLoansByUserId(user.Id);
                    return View(payment);
                }
                return RedirectToRoute(new { controller = "Client", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        #endregion
    }
}

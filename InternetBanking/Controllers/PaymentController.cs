using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment;
using Microsoft.AspNetCore.Mvc;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Strategy.Context;
using InternetBanking.Core.Application.Strategy.Methods;

namespace WebApp.InternetBanking.Controllers
{
    [Authorize(Roles = "Client")]
    public class PaymentController : Controller
    {
        private readonly PaymentContext _paymentContext;
        private readonly IPaymentService _paymentService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IMoneyLoanService _moneyLoanService;
        private readonly ICreditCardsService _creditCardsService;
        private readonly AuthenticationResponse? user;

        public PaymentController(IPaymentService paymentService, ISavingAccountService savingAccountService, IHttpContextAccessor contextAccessor,
            IBeneficiaryService beneficiaryService, IMoneyLoanService moneyLoanService, ICreditCardsService creditCardsService, PaymentContext paymentContext)
        {
            _paymentService = paymentService;
            _savingAccountService = savingAccountService;
            _contextAccessor = contextAccessor;
            _beneficiaryService = beneficiaryService;
            user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _moneyLoanService = moneyLoanService;
            _creditCardsService = creditCardsService;
            _paymentContext = paymentContext;
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
                model.TypeOfPayment = TypeOfPayment.Express.ToString();
                _paymentContext.SetStrategy(new BeneficiaryExpressPayment(_savingAccountService, _paymentService));
                await _paymentContext.MakePayment(model);
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
                model.TypeOfPayment = TypeOfPayment.Beneficiary.ToString();
                _paymentContext.SetStrategy(new BeneficiaryExpressPayment(_savingAccountService, _paymentService));
                await _paymentContext.MakePayment(model);
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
                model.IdUser = user.Id;
                _paymentContext.SetStrategy(new MoneyLoanPayment(_savingAccountService, _moneyLoanService, _paymentService));
                var payment = await _paymentContext.MakePayment(model);

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

        #region Credit Card

        public async Task<IActionResult> CreditCardPayment()
        {
            try
            {
                ViewBag.CreditCard = await _creditCardsService.GetCreditCardsByUserId(user.Id);
                ViewBag.Account = await _savingAccountService.GetAccountsByUserId(user.Id);
                return View("CreditCardPayment");

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreditCardPayment(SavePaymentViewModel viewModel)
        {
            try
            {
                viewModel.IdUser = user.Id;
                if (!ModelState.IsValid)
                {

                    ViewBag.CreditCard = await _creditCardsService.GetCreditCardsByUserId(user.Id);
                    ViewBag.Account = await _savingAccountService.GetAccountsByUserId(user.Id);
                    return View(viewModel);

                }

                viewModel.IdUser = user.Id;
                _paymentContext.SetStrategy(new CreditCardPayment(_paymentService, _savingAccountService, _creditCardsService));
                var payment = await _paymentContext.MakePayment(viewModel);

                if (payment.HasError)
                {
                    payment.HasError = viewModel.HasError;
                    payment.Error = viewModel.Error;
                    ViewBag.CreditCard = await _creditCardsService.GetCreditCardsByUserId(user.Id);
                    ViewBag.Account = await _savingAccountService.GetAccountsByUserId(user.Id);

                    return View("CreditCardPayment", payment);
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

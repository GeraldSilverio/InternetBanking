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
        public PaymentController(IPaymentService paymentService, ISavingAccountService savingAccountService, IHttpContextAccessor contextAccessor)
        {
            _paymentService = paymentService;
            _savingAccountService = savingAccountService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> ExpressPayment()
        {
            var user = _contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
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
                    return View("ExpressPayment", model);
                }

                var payment = await _paymentService.ValidatePayment(model);

                if (payment.HasError)
                {
                    return View("ExpressPayment", payment);
                }
                return View("ConfirmExpressPayment", payment);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }

        public IActionResult ConfirmExpressPayment()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmExpressPayment(SavePaymentViewModel model)
        {
            try
            {
                await _paymentService.ExpressPayment(model);
                return RedirectToRoute(new { controller = "Client", action = "Index" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }
    }
}

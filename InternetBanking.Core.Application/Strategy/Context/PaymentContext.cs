using InternetBanking.Core.Application.Strategy.Interfaces;
using InternetBanking.Core.Application.ViewModels.Payment;

namespace InternetBanking.Core.Application.Strategy.Context
{
    public class PaymentContext
    {
        private IPayment _payment;

        public PaymentContext(IPayment payment)
        {
            _payment = payment;
        }

        public void SetStrategy(IPayment payment)
        {
            _payment = payment;
        }

        public async Task<SavePaymentViewModel> MakePayment(SavePaymentViewModel model)
        {
           return await _payment.MakePayment(model);
        }
    }
}

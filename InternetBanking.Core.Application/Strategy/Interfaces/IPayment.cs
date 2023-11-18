using InternetBanking.Core.Application.ViewModels.Payment;

namespace InternetBanking.Core.Application.Strategy.Interfaces
{
    public interface IPayment
    {
        Task<SavePaymentViewModel> MakePayment(SavePaymentViewModel model);
    }
}



using InternetBanking.Core.Application.ViewModels.Payment;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IValidatePayment
    {
        Task<SavePaymentViewModel> ValidateExpressPayment(SavePaymentViewModel viewModel);
        Task<SavePaymentViewModel> ValidateBeneficiaryPayment(SavePaymentViewModel viewModel);
    }
}

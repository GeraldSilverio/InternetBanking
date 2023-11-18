using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IPaymentService : IGenericService<Payment, SavePaymentViewModel, PaymentViewModel>
    {
        Task<SavePaymentViewModel> ValidateExpressPayment(SavePaymentViewModel model);
        Task Payment(SavePaymentViewModel viewModel);
        Task<SavePaymentViewModel> MoneyLoanPayment(SavePaymentViewModel viewModel);
        Task <SavePaymentViewModel> ValidateBeneficiaryPayment(SavePaymentViewModel viewModel);
        Task<SavePaymentViewModel> CreditCardPayment(SavePaymentViewModel viewModel);
        int GetCountPayment();
        int GetPaymentPerDay();
    }
}

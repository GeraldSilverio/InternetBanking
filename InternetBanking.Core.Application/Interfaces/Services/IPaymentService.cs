using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IPaymentService : IGenericService<Payment, SavePaymentViewModel, PaymentViewModel>
    {
        Task<SavePaymentViewModel> ValidatePayment(SavePaymentViewModel model);
        Task ExpressPayment(SavePaymentViewModel viewModel);


    }
}

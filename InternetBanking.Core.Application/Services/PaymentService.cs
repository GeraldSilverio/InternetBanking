using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class PaymentService : GenericService<Payment, SavePaymentViewModel, PaymentViewModel>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepositoy;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, IPaymentRepository paymentRepositoy) : base(paymentRepository, mapper)
        {
            _paymentRepositoy = paymentRepositoy;
        }

        public int GetCountPayment()
        {
            return _paymentRepositoy.GetCount();
        }

        public int GetPaymentPerDay()
        {
            return _paymentRepositoy.GetPaymentPerDay();
        }


    }
}

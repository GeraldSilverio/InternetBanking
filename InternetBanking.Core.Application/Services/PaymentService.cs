
using AutoMapper;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using InternetBanking.Core.Application.ViewModels.SavingAccount;
using InternetBanking.Core.Application.Enums;

namespace InternetBanking.Core.Application.Services
{
    public class PaymentService : GenericService<Payment, SavePaymentViewModel, PaymentViewModel>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepositoy;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly AuthenticationResponse? user;
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, ISavingAccountService savingAccountService, IAccountService accountService, IHttpContextAccessor httpContextAccessor,
            IBeneficiaryService beneficiaryService, IPaymentRepository paymentRepositoy) : base(paymentRepository, mapper)
        {
            _savingAccountService = savingAccountService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _beneficiaryService = beneficiaryService;
            _paymentRepositoy = paymentRepositoy;
        }

        public async Task<SavePaymentViewModel> ValidateExpressPayment(SavePaymentViewModel model)
        {
            var originAccount = await _savingAccountService.GetByAccountCode(model.OriginAccount);
            var destinationAccount = await _savingAccountService.GetByAccountCode(model.DestinationAccount);

            //Validando que la cuenta de origen exista.
            if (originAccount is null)
            {
                model.HasError = true;
                model.Error = "LA CUENTA DE ORIGEN NO EXISTE";
                return model;
            }
            //Validar que la cuenta de origen tenga el dinero necesario.
            if (originAccount.Balance < model.Amount)
            {
                model.HasError = true;
                model.Error = "LA CUENTA DE ORIGEN NO POSEE EL DINERO SUFICIENTE PARA ESTE PAGO";
                return model;
            }
            //Validar que la cuenta de destino exista
            if (destinationAccount is null)
            {

                model.HasError = true;
                model.Error = "LA CUENTA DE DESTINO NO EXISTE";
                return model;
            }
            var ownerDestinationAccount = await _accountService.GetUserByIdAsync(destinationAccount.IdUser);
            model.FullName = ownerDestinationAccount.FirstName + " " + ownerDestinationAccount.LastName;
            model.IdUser = user.Id;
            model.TypeOfPayment = TypeOfPayment.Express.ToString();
            return model;
        }


        #region BeneficiaryPayment
        public async Task<SavePaymentViewModel> ValidateBeneficiaryPayment(SavePaymentViewModel model)
        {
            //Cuenta original.
            var originAccount = await _savingAccountService.GetByAccountCode(model.OriginAccount);
            //Beneficiario
            var beneficiary = await _beneficiaryService.GetByAccountCode(model.DestinationAccount);
            //El user del beneficiario 
            var ownerDestinationAccount = await _accountService.GetUserByIdAsync(beneficiary.IdBeneficiary);


            if (originAccount.Balance < model.Amount)
            {
                model.HasError = true;
                model.Error = "LA CUENTA DE ORIGEN NO POSEE EL DINERO SUFICIENTE PARA ESTE DEPOSITO";
                return model;
            }

            model.FullName = ownerDestinationAccount.FirstName + " " + ownerDestinationAccount.LastName;
            model.IdUser = user.Id;
            model.TypeOfPayment = TypeOfPayment.Beneficiary.ToString();
            return model;
        }
        #endregion

       
       

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

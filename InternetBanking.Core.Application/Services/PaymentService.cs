
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
        private readonly ISavingAccountService _savingAccountService;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private AuthenticationResponse user;
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, ISavingAccountService savingAccountService, IAccountService accountService, IHttpContextAccessor httpContextAccessor, IBeneficiaryService beneficiaryService) : base(paymentRepository, mapper)
        {
            _savingAccountService = savingAccountService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _beneficiaryService = beneficiaryService;
        }


        #region ExpressPayment
        public async Task Payment(SavePaymentViewModel model)
        {
            try
            {
                var originAccount = _mapper.Map<CreateSavingAccountViewModel>(await _savingAccountService.GetByAccountCode(model.OriginAccount));
                var destinationAccount = _mapper.Map<CreateSavingAccountViewModel>(await _savingAccountService.GetByAccountCode(model.DestinationAccount));

                //Efectuar Transaccion.
                originAccount.Balance -= model.Amount;
                destinationAccount.Balance += model.Amount;
                //Actualizando cuentas.
                await _savingAccountService.Update(originAccount, originAccount.Id);
                await _savingAccountService.Update(destinationAccount, destinationAccount.Id);
                //Agregando registro de pago.
                await base.Add(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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
        #endregion

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
    }
}

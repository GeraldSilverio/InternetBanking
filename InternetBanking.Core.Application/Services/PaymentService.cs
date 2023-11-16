
using AutoMapper;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using InternetBanking.Core.Application.ViewModels.SavingAccount;

namespace InternetBanking.Core.Application.Services
{
    public class PaymentService : GenericService<Payment, SavePaymentViewModel, PaymentViewModel>, IPaymentService
    {
        private readonly ISavingAccountService _savingAccountService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, ISavingAccountService savingAccountService, IAccountService accountService, IHttpContextAccessor httpContextAccessor) : base(paymentRepository, mapper)
        {
            _savingAccountService = savingAccountService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task ExpressPayment(SavePaymentViewModel model)
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

        public async Task<SavePaymentViewModel> ValidatePayment(SavePaymentViewModel model)
        {
            var user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var originAccount = await _savingAccountService.GetByAccountCode(model.OriginAccount);
            var destinationAccount = await _savingAccountService.GetByAccountCode(model.DestinationAccount);
            var ownerDestinationAccount = await _accountService.GetUserByIdAsync(destinationAccount.IdUser);

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

            model.FullName = ownerDestinationAccount.FirstName + " " + ownerDestinationAccount.LastName;
            model.IdUser = user.Id;
            return model;
        }
    }
}

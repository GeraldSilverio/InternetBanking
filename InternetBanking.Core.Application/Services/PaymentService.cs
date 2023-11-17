
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
using System.Runtime.CompilerServices;

namespace InternetBanking.Core.Application.Services
{
    public class PaymentService : GenericService<Payment, SavePaymentViewModel, PaymentViewModel>, IPaymentService
    {
        private readonly ISavingAccountService _savingAccountService;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IAccountService _accountService;
        private readonly ICreditCardsService _creditCardsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IMoneyLoanService _moneyLoanService;

        private readonly AuthenticationResponse? user;
        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper, ISavingAccountService savingAccountService, IAccountService accountService, IHttpContextAccessor httpContextAccessor, 
            IBeneficiaryService beneficiaryService, IMoneyLoanService moneyLoanService, ICreditCardsService creditCardsService) : base(paymentRepository, mapper)
        {
            _savingAccountService = savingAccountService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _beneficiaryService = beneficiaryService;
            _moneyLoanService = moneyLoanService;
            _creditCardsService = creditCardsService;
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

        #region MoneyPayment
        public async Task<SavePaymentViewModel> MoneyLoanPayment(SavePaymentViewModel viewModel)
        {
            try
            {
                viewModel.IdUser = user.Id;
                var originAccount = _mapper.Map<CreateSavingAccountViewModel>(await _savingAccountService.GetByAccountCode(viewModel.OriginAccount));
                var moneyLoan = await _moneyLoanService.GetById(viewModel.DestinationAccount);
                decimal vaApagar = moneyLoan.BalancePaid + viewModel.Amount;

                if (moneyLoan.BalancePaid == moneyLoan.BorrowedBalance)
                {
                    viewModel.HasError = true;
                    viewModel.Error = "YA HAS PAGADO EL MONTO TOTAL DEL PRESTAMO";
                    return viewModel;
                }

                //Validar que la cuenta tenga el dinero suficiente.
                if (originAccount.Balance < viewModel.Amount)
                {
                    viewModel.HasError = true;
                    viewModel.Error = "LA CUENTA NO POSEE EL DINERO SUFICIENTE PARA REALIZAR EL PAGO";
                    return viewModel;
                }

                //Validar que no le pague mas de lo que se le debe
                if (vaApagar > moneyLoan.BorrowedBalance)
                {
                    //Lo estaria pagando de mas
                    var residuo = vaApagar - moneyLoan.BorrowedBalance;
                    //Actulizamos lo que se va a cobrar
                    moneyLoan.BalancePaid += residuo;
                    originAccount.Balance -= residuo;
                }
                else
                {
                    moneyLoan.BalancePaid += viewModel.Amount;
                    originAccount.Balance -= viewModel.Amount;
                }
               
                //Efectuar las transacciones.
                await _moneyLoanService.Update(moneyLoan, moneyLoan.Id);
                await _savingAccountService.Update(originAccount, originAccount.Id);
                //Agregar el registro del pago.
                return await base.Add(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return viewModel;
            }

        }
        #endregion

        #region Credit Card Payment
        public async Task<SavePaymentViewModel> CreditCardPayment(SavePaymentViewModel viewModel)
        {
            try
            {
                viewModel.IdUser = user.Id;
                var originAccount = await _savingAccountService.GetById(viewModel.OriginAccount);
                var creditCard = await _creditCardsService.GetById(viewModel.DestinationAccount);

                if (originAccount.Balance < viewModel.Amount)
                {
                    viewModel.HasError = true;
                    viewModel.Error = "LA CUENTA DE AHORRO SELECCIONADA NO POSEE EL DINERO SUFICIENTE PARA ESTE DEPOSITO";
                    return viewModel;
                } 

                if (viewModel.Amount > creditCard.Debt)
                {
                    var payment = viewModel.Amount - creditCard.Debt;
                    originAccount.Balance -= viewModel.Amount;     
                    originAccount.Balance += payment;
                    creditCard.Debt = 0.00m;
                }
                else
                {
                    originAccount.Balance -= viewModel.Amount;
                    creditCard.Debt -= viewModel.Amount;
                }

                await _savingAccountService.Update(originAccount, originAccount.Id);
                await _creditCardsService.Update(creditCard, creditCard.Id);

                return await base.Add(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return viewModel;
            }

        }
        #endregion
    }
}

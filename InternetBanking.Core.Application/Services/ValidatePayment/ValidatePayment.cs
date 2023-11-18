using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment;

namespace InternetBanking.Core.Application.Services.ValidatePayment
{
    public class ValidatePayment : IValidatePayment
    {
        private readonly IAccountService _accountService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IBeneficiaryService _beneficiaryService;

        public ValidatePayment(IAccountService accountService, ISavingAccountService savingAccountService, IBeneficiaryService beneficiaryService)
        {
            _accountService = accountService;
            _savingAccountService = savingAccountService;
            _beneficiaryService = beneficiaryService;
        }
        #region Beneficiary
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
            model.TypeOfPayment = TypeOfPayment.Beneficiary.ToString();
            return model;
        }
        #endregion

        #region Express
        public async  Task<SavePaymentViewModel> ValidateExpressPayment(SavePaymentViewModel model)
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
            model.TypeOfPayment = TypeOfPayment.Express.ToString();
            return model;
        }
        #endregion
    }
}

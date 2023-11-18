using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Strategy.Interfaces;
using InternetBanking.Core.Application.ViewModels.Payment;

namespace InternetBanking.Core.Application.Strategy.Methods
{
    public class BeneficiaryExpressPayment : IPayment
    {
        private readonly ISavingAccountService _savingAccountService;
        private readonly IPaymentService _paymentService;

        public BeneficiaryExpressPayment(ISavingAccountService savingAccountService, IPaymentService paymentService)
        {
            _savingAccountService = savingAccountService;
            _paymentService = paymentService;
        }

        public async Task<SavePaymentViewModel> MakePayment(SavePaymentViewModel model)
        {
            try
            {
                var originAccount = await _savingAccountService.GetByAccountCode(model.OriginAccount);
                var destinationAccount = await _savingAccountService.GetByAccountCode(model.DestinationAccount);

                //Efectuar Transaccion.
                originAccount.Balance -= model.Amount;
                destinationAccount.Balance += model.Amount;
                //Actualizando cuentas.
                await _savingAccountService.Update(originAccount, originAccount.Id);
                await _savingAccountService.Update(destinationAccount, destinationAccount.Id);
                //Agregando registro de pago.
                return await _paymentService.Add(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return model;
            }
        }
    }
}

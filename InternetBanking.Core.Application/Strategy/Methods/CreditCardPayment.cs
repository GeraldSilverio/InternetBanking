using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Strategy.Interfaces;
using InternetBanking.Core.Application.ViewModels.Payment;
using Microsoft.AspNetCore.Http;


namespace InternetBanking.Core.Application.Strategy.Methods
{
    public class CreditCardPayment : IPayment
    {
        private readonly IPaymentService _paymentService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly ICreditCardsService _creditCardsService;


        public CreditCardPayment(IPaymentService paymentService, ISavingAccountService savingAccountService, ICreditCardsService creditCardsService)
        {
            _paymentService = paymentService;
            _savingAccountService = savingAccountService;
            _creditCardsService = creditCardsService;
        }
      

        public async Task<SavePaymentViewModel> MakePayment(SavePaymentViewModel viewModel)
        {
            try
            {
                viewModel.TypeOfPayment = TypeOfPayment.CreditCard.ToString();
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
                    viewModel.Amount = creditCard.Debt;
                    originAccount.Balance -= viewModel.Amount;
                    creditCard.Debt -= viewModel.Amount;
                    creditCard.Available += viewModel.Amount;

                }
                else
                {
                    originAccount.Balance -= viewModel.Amount;
                    creditCard.Debt -= viewModel.Amount;
                    creditCard.Available += viewModel.Amount;
                }

                await _savingAccountService.Update(originAccount, originAccount.Id);
                await _creditCardsService.Update(creditCard, creditCard.Id);

                return await _paymentService.Add(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return viewModel;
            }
        }
    }
}

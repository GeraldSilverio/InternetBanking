using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Strategy.Interfaces;
using InternetBanking.Core.Application.ViewModels.Payment;

namespace InternetBanking.Core.Application.Strategy.Methods
{
    public class MoneyLoanPayment : IPayment
    {
        private readonly ISavingAccountService _savingAccountService;
        private readonly IMoneyLoanService _moneyLoanService;
        private readonly IPaymentService _paymentService;

        public MoneyLoanPayment(ISavingAccountService savingAccountService, IMoneyLoanService moneyLoanService, IPaymentService paymentService)
        {
            _savingAccountService = savingAccountService;
            _moneyLoanService = moneyLoanService;
            _paymentService = paymentService;
        }

        public async Task<SavePaymentViewModel> MakePayment(SavePaymentViewModel viewModel)
        {
            try
            {
                viewModel.TypeOfPayment = TypeOfPayment.MoneyLoan.ToString();
                var originAccount = await _savingAccountService.GetByAccountCode(viewModel.OriginAccount);
                var moneyLoan = await _moneyLoanService.GetById(viewModel.DestinationAccount);
                decimal totalToPay = moneyLoan.BalancePaid + viewModel.Amount;

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
                if (totalToPay > moneyLoan.BorrowedBalance)
                {
                    //Lo estaria pagando de mas
                    decimal excessAmount = totalToPay - moneyLoan.BorrowedBalance;
                    //Actulizamos lo que se va a cobrar
                    moneyLoan.BalancePaid += viewModel.Amount - excessAmount;
                    originAccount.Balance -= viewModel.Amount - excessAmount;
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

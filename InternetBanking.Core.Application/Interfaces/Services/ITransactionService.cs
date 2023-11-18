using InternetBanking.Core.Application.ViewModels.Payment.Transaction;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ITransactionService:IGenericService<Transaction,SaveTransactionViewModel,TransactionViewModel>
    {
        Task<SaveTransactionViewModel> AddTransaction(SaveTransactionViewModel model);
        int GetCountTransaction();
        int GetTransactionPerDay();
    }
}

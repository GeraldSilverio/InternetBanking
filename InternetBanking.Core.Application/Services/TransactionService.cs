using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Transactions;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class TransactionService : GenericService<Transaction, SaveTransactionViewModel, TransactionViewModel>, ITransactionService
    {
        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper) : base(transactionRepository, mapper)
        {
        }
    }
}

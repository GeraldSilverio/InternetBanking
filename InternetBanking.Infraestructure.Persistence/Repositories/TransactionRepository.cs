using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infraestructure.Persistence.Contexts;

namespace InternetBanking.Infraestructure.Persistence.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}

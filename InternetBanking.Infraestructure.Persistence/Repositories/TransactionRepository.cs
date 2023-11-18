using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infraestructure.Persistence.Contexts;

namespace InternetBanking.Infraestructure.Persistence.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly ApplicationContext _dbContext;
        public TransactionRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public int GetTransactionPerDay()
        {
            return _dbContext.Transactions.Where(x=> x.Date == DateTime.Now.Date).Count();
        }
    }
}

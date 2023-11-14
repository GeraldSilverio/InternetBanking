using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infraestructure.Persistence.Contexts;

namespace InternetBanking.Infraestructure.Persistence.Repositories
{
    public class MoneyLoanRepository : GenericRepository<MoneyLoan>, IMoneyLoanRepository
    {
        private readonly ApplicationContext _dbContext;

        public MoneyLoanRepository (ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

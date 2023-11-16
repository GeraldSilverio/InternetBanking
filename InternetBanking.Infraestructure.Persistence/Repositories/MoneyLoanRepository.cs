using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infraestructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infraestructure.Persistence.Repositories
{
    public class MoneyLoanRepository : GenericRepository<MoneyLoan>, IMoneyLoanRepository
    {
        private readonly ApplicationContext _dbContext;

        public MoneyLoanRepository (ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MoneyLoan>> GetMoneyLoanByUserIdAsync(string idUser)
        {
            return await _dbContext.MoneyLoans
                .Where(account => account.IdUser == idUser)
                .ToListAsync();
        }
    }
}

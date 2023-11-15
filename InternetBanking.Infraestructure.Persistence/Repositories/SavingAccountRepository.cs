using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infraestructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infraestructure.Persistence.Repositories
{
    public class SavingAccountRepository : GenericRepository<SavingAccount>, ISavingAccountRepository
    {
        private readonly ApplicationContext _dbContext;

        public SavingAccountRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SavingAccount> GetByIdUser(string idUser)
        {
            return await _dbContext.SavingAccounts.FirstOrDefaultAsync(x => x.IdUser == idUser && x.IsPrincipal == true);
        }

        public async Task<List<SavingAccount>> GetAccountsByUserId(string idUser)
        {
            return await _dbContext.SavingAccounts
                .Where(account => account.IdUser == idUser)
                .ToListAsync();
        }

        public bool HaveAccount(string idUser)
        {
            var haveAccount = _dbContext.SavingAccounts.Any(x => x.IdUser == idUser);
            return haveAccount;
        }
    }
}

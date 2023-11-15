using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infraestructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infraestructure.Persistence.Repositories
{
    public class CreditsCardRepository : GenericRepository<CreditsCard>, ICreditsCardRepository
    {
        private readonly ApplicationContext _dbContext;

        public CreditsCardRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CreditsCard>> GetCreditCardsByUserIdAsync(string idUser)
        {
            return await _dbContext.CreditsCards
                .Where(account => account.IdUser == idUser)
                .ToListAsync();
        }

    }
}

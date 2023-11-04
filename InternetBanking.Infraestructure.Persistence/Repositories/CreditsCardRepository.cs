using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infraestructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Infraestructure.Persistence.Repositories
{
    public class CreditsCardRepository : GenericRepository<CreditsCard>, ICreditsCardRepository
    {
        private readonly ApplicationContext _dbContext;

        public CreditsCardRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

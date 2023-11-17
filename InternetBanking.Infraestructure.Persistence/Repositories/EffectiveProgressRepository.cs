
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infraestructure.Persistence.Contexts;

namespace InternetBanking.Infraestructure.Persistence.Repositories
{
    public class EffectiveProgressRepository : GenericRepository<EffectiveProgress>, IEffectiveProgressRepository
    {

        public EffectiveProgressRepository(ApplicationContext context) : base (context)
        {
            
        }
    }
}

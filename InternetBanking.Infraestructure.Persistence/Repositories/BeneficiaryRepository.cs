using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infraestructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infraestructure.Persistence.Repositories
{
    public class BeneficiaryRepository : GenericRepository<Beneficiary>, IBeneficiaryRepository
    {
        private readonly ApplicationContext _dbContext;

        public BeneficiaryRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Beneficiary>> GetAllByUser(string idUser)
        {
            var beneficiaries = await _dbContext.Beneficiaries.Where(x => x.IdUser == idUser).ToListAsync();
            return beneficiaries;
        }

        public async Task<Beneficiary> GetByAccountCode(int accountCode)
        {
            return await _dbContext.Beneficiaries.FirstOrDefaultAsync(x => x.AccountCode == accountCode);
        }

        public async Task<bool> IsBeneficiaryAdd(string idUser, string idBeneficiary, int accountCode)
        {
            var beneficiaries = await _dbContext.Beneficiaries.ToListAsync();

            foreach (var beneficiary in beneficiaries)
            {
                if (beneficiary.IdUser == idUser && beneficiary.IdBeneficiary == idBeneficiary && beneficiary.AccountCode == accountCode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

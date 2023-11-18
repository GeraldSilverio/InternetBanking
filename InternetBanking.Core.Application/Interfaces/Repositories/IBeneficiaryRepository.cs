using InternetBanking.Core.Application.ViewModels.Beneficiary;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface IBeneficiaryRepository : IGenericRepository<Beneficiary>
    {
        Task<List<Beneficiary>> GetAllByUser(string idUser);
        Task<bool> IsBeneficiaryAdd(string idUser, string idBeneficiary,int accountCode);
        Task<Beneficiary> GetByAccountCode(int accountCode);
    }
}

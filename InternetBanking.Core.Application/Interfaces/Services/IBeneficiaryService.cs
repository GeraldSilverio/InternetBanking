using InternetBanking.Core.Application.ViewModels.Beneficiary;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IBeneficiaryService : IGenericService<Beneficiary, SaveBeneficiaryViewModel, BeneficiaryViewModel>
    {
        Task<List<BeneficiaryViewModel>> GetAllByUser(string id);
        Task<BeneficiaryViewModel> GetByAccountCode(int accountCode);
    }
}

using InternetBanking.Core.Application.ViewModels.Filter;
using InternetBanking.Core.Application.ViewModels.SavingAccount;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ISavingAccountService:IGenericService<SavingAccount,CreateSavingAccountViewModel,SavingAccountViewModel>
    {
        Task<SavingAccountViewModel> GetByIdUser(string id);
        Task UpdatePrincialAccount(decimal balance, string IdUser);
        Task<CreateSavingAccountViewModel> GetByAccountCode(int accountCode);
        Task<List<SavingAccountViewModel>> GetAccountsByUserId(string idUser);

        Task<List<SavingAccountViewModel>> GetAllWithFilters(FilterIdentityCardViewModel filters);
    }
}

using InternetBanking.Core.Application.ViewModels.SavingAccount;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ISavingAccountService:IGenericService<SavingAccount,CreateSavingAccountViewModel,SavingAccountViewModel>
    {
        Task<SavingAccountViewModel> GetByIdUser(string id);
        Task UpdatePrincialAccount(decimal balance, string IdUser);
        Task<List<SavingAccountViewModel>> GetAccountById(string id);
        Task<SavingAccount> GetByAccountCode(int accountCode);


    }
}

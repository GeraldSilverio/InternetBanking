using InternetBanking.Core.Application.ViewModels.SavingAccount;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ISavingAccountService:IGenericService<SavingAccount,CreateSavingAccountViewModel,SavingAccountViewModel>
    {
    }
}

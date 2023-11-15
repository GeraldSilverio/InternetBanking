using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface ISavingAccountRepository : IGenericRepository<SavingAccount>
    {
        bool HaveAccount(string idUser);

        Task<SavingAccount> GetByIdUser(string idUser);
        Task<List<SavingAccount>> GetAccountsByUserId(string idUser);
    }
}

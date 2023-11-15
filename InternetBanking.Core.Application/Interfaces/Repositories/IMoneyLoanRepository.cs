using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Repositories
{
    public interface IMoneyLoanRepository : IGenericRepository<MoneyLoan>
    {
        Task<List<MoneyLoan>> GetMoneyLoanByUserIdAsync(string idUser);
    }
}

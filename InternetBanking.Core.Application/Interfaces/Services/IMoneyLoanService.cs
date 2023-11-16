using InternetBanking.Core.Application.ViewModels.MoneyLoan;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IMoneyLoanService:IGenericService<MoneyLoan, NewMoneyLoanViewModel, MoneyLoanViewModel>
    {
        Task<List<MoneyLoanViewModel>> GetMoneyLoansByUserId(string id);
    }
}

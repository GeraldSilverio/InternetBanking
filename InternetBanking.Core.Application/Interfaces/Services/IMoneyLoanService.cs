using InternetBanking.Core.Application.ViewModels.Filter;
using InternetBanking.Core.Application.ViewModels.MoneyLoan;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IMoneyLoanService:IGenericService<MoneyLoan, NewMoneyLoanViewModel, MoneyLoanViewModel>
    {
        Task<List<MoneyLoanViewModel>> GetMoneyLoansByUserId(string id);
        Task<List<MoneyLoanViewModel>> GetAllWithFilters(FilterIdentityCardViewModel filters);
        Task<List<MoneyLoanViewModel>> GetMoneyLoansById(string id);
    }
}

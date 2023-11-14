using AutoMapper;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.CreditCards;
using InternetBanking.Core.Application.ViewModels.MoneyLoan;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class MoneyLoanService : GenericService<MoneyLoan, NewMoneyLoanViewModel, MoneyLoanViewModel>, IMoneyLoanService
    {
        private readonly ISavingAccountService _savingAccountService;
        private readonly IAccountService _accountService;
        public MoneyLoanService(IMoneyLoanRepository moneyLoanRepository, IMapper mapper, ISavingAccountService savingAccountService, IAccountService accountService) : base(moneyLoanRepository, mapper)
        {
            _savingAccountService = savingAccountService;
            _accountService = accountService;
        }

        public override async Task<NewMoneyLoanViewModel> Add(NewMoneyLoanViewModel model)
        {
            model.MoneyLoanCode = GenerateCode.GenerateAccountCode(model.CurrentDate);
            var moneyloan =await base.Add(model);
            await _savingAccountService.UpdatePrincialAccount(moneyloan.BorrowedBalance, moneyloan.IdUser);
            return moneyloan;
        }

        public override async Task<List<MoneyLoanViewModel>> GetAll()
        {
            var moneyLoansList = new List<MoneyLoanViewModel>();
            var moneyLoans = await base.GetAll();
            

            foreach (var moneyLoan in moneyLoans)
            {
                var user = await _accountService.GetUserByIdAsync(moneyLoan.IdUser);
                var moneyLoanView = new MoneyLoanViewModel()
                {
                    Id = moneyLoan.Id,
                    IdUser = moneyLoan.IdUser,
                    UserFullName = user.FirstName + " " + user.LastName,
                    UserIdentityCard = user.IdentityCard,
                    MoneyLoanCode = moneyLoan.MoneyLoanCode,
                    BorrowedBalance = moneyLoan.BorrowedBalance,
                    BalancePaid = moneyLoan.BalancePaid,
                    Debt = moneyLoan.BorrowedBalance -moneyLoan.BalancePaid,
                };
                moneyLoansList.Add(moneyLoanView);
            }

            return moneyLoansList;
        }
    }
}

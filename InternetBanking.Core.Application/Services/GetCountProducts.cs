using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;

namespace InternetBanking.Core.Application.Services
{
    public class GetCountProducts : IGetCountProduct
    {
        private readonly IMoneyLoanRepository _moneyLoanRepository;
        private readonly ISavingAccountRepository _savingAccountRepository;
        private readonly ICreditsCardRepository _creditsCardRepository;

        public GetCountProducts(IMoneyLoanRepository moneyLoanRepository, ISavingAccountRepository savingAccountRepository, ICreditsCardRepository creditsCardRepository)
        {
            _moneyLoanRepository = moneyLoanRepository;
            _savingAccountRepository = savingAccountRepository;
            _creditsCardRepository = creditsCardRepository;
        }

        public int GetCount()
        {
            var creditCards = _creditsCardRepository.GetCount();
            var savingAccount = _savingAccountRepository.GetCount();
            var moneyLoans = _moneyLoanRepository.GetCount();

            return creditCards + savingAccount + moneyLoans;
        }
    }
}

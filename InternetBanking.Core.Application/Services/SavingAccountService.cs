using AutoMapper;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Filter;
using InternetBanking.Core.Application.ViewModels.SavingAccount;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class SavingAccountService : GenericService<SavingAccount, CreateSavingAccountViewModel, SavingAccountViewModel>, ISavingAccountService
    {
        private readonly ISavingAccountRepository _savingAccountrepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public SavingAccountService(ISavingAccountRepository savingAccountRepository, IMapper mapper, ISavingAccountRepository savingAccountrepository, IAccountService accountService) : base(savingAccountRepository, mapper)
        {
            _savingAccountrepository = savingAccountrepository;
            _accountService = accountService;
            _mapper = mapper;
        }

        public override Task<CreateSavingAccountViewModel> Add(CreateSavingAccountViewModel model)
        {
            model.AccountCode = GenerateCode.GenerateAccountCode(model.CurrentDate);
            var haveAccount = _savingAccountrepository.HaveAccount(model.IdUser);
            if (haveAccount != true)
            {
                model.IsPrincipal = true;
            }
            return base.Add(model);
        }

        public async Task<List<SavingAccountViewModel>> GetAllWithFilters(FilterIdentityCardViewModel filters)
        {
            var accountList = new List<SavingAccountViewModel>();
            var savingAccount = await _savingAccountrepository.GetAllAsync();

            foreach (var account in savingAccount)
            {
                var user = await _accountService.GetUserByIdAsync(account.IdUser);
                var accountView = new SavingAccountViewModel()
                {
                    Id = account.Id,
                    IdUser = account.IdUser,
                    FullName = user.FirstName + " " + user.LastName,
                    IdentityCard = user.IdentityCard,
                    AccountCode = account.AccountCode,
                    Balance = account.Balance,
                    IsPrincial = account.IsPrincipal
                };
                accountList.Add(accountView);
            }

            if (!string.IsNullOrEmpty(filters.IdentityCard))
            {
                string identityCardToLower = filters.IdentityCard.ToLower();
                accountList = accountList.Where(lr => lr.IdentityCard.ToLower().Contains(identityCardToLower)).ToList();
            }

            return accountList;
        }

        public override Task<CreateSavingAccountViewModel> GetById(int id)
        {

            return base.GetById(id);
        }

        public async Task<SavingAccountViewModel> GetByIdUser(string id)
        {
            var account = await _savingAccountrepository.GetByIdUser(id);
            return _mapper.Map<SavingAccountViewModel>(account);
        }

      
        public async Task UpdatePrincialAccount(decimal balance, string IdUser)
        {
            var principalAccount = await _savingAccountrepository.GetByIdUser(IdUser);
            principalAccount.Balance += balance;
            await _savingAccountrepository.UpdateAsync(principalAccount, principalAccount.Id);
        }

        public async Task<SavingAccount> GetByAccountCode(int accountCode)
        {
            return await _savingAccountrepository.GetByAccountCode(accountCode);
        }

        public async Task<List<SavingAccountViewModel>> GetAccountsByUserId(string idUser)
        {
            var accounts = await _savingAccountrepository.GetAccountsByUserId(idUser);
            return _mapper.Map<List<SavingAccountViewModel>>(accounts); 
        }
    }
}

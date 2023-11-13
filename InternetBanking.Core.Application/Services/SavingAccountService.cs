using AutoMapper;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.SavingAccount;
using InternetBanking.Core.Domain.Entities;
using System.Security.Principal;

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
            model.AccountCode = GenerateCode.GenerateAccountCode(model);
            var haveAccount = _savingAccountrepository.HaveAccount(model.IdUser);
            if (haveAccount != true)
            {
                model.IsPrincipal = true;
            }
            return base.Add(model);
        }

        public override async Task<List<SavingAccountViewModel>> GetAll()
        {
            var accountList = new List<SavingAccountViewModel>();
            var savingAccount = await _savingAccountrepository.GetAllAsync();

            foreach (var account in savingAccount)
            {
                var user = _accountService.GetUserByIdAsync(account.IdUser);
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
    }
}

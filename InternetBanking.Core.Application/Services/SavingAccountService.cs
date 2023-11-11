using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.SavingAccount;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class SavingAccountService : GenericService<SavingAccount, CreateSavingAccountViewModel, SavingAccountViewModel>,ISavingAccountService
    {
        private readonly ISavingAccountRepository _savingAccountrepository;
        public SavingAccountService(ISavingAccountRepository savingAccountRepository, IMapper mapper, ISavingAccountRepository savingAccountrepository) : base(savingAccountRepository, mapper)
        {
            _savingAccountrepository = savingAccountrepository;
        }

        public override Task<CreateSavingAccountViewModel> Add(CreateSavingAccountViewModel model)
        {
            var haveAccount = _savingAccountrepository.HaveAccount(model.IdUser);
            if(haveAccount != true)
            {
                model.IsPrincipal = true;
            }
            return base.Add(model);
        }
    }
}

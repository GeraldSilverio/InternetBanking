using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Beneficiary;
using InternetBanking.Core.Application.ViewModels.CreditCards;
using InternetBanking.Core.Application.ViewModels.Payment.EffectiveProgress;
using InternetBanking.Core.Application.ViewModels.SavingAccount;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class EffectiveProgressService : GenericService<EffectiveProgress, SaveEffectiveProgressViewModel, EffectiveProgress> , IEffectiveProgressService
    {
        private readonly ICreditCardsService _creditCardsService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private AuthenticationResponse User;
        private readonly IMapper _mapper;
        public EffectiveProgressService
            (IEffectiveProgressRepository effectiveProgressRepository, IMapper mapper, ICreditCardsService creditCardsService, ISavingAccountService savingAccountService,
            IAccountService accountService, IHttpContextAccessor httpContextAccessor)
            : base(effectiveProgressRepository,mapper)
        {            
            _creditCardsService = creditCardsService;
            _savingAccountService = savingAccountService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            User = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task AddEffectiveProgress(SaveEffectiveProgressViewModel model)
        {
            try
            {
                var originAccount = _mapper.Map<SaveCardViewModel>(await _savingAccountService.GetByAccountCode(model.OriginAccount));
                var destinationAccount = _mapper.Map<CreateSavingAccountViewModel>(await _savingAccountService.GetByAccountCode(model.DestinationAccount));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        //public async Task<List<EffectiveProgress>> GetAllByUser(string id)
        //{
        //    var effectivelist = new List<EffectiveProgress>();
        //    var effective = await _beneficiaryRepository.GetAllByUser(id);
        //    foreach (var e in effective)
        //    {
        //        var beneficiary = await _accountService.GetUserByIdAsync(benef.IdBeneficiary);
        //        var beneficiaryView = new EffectiveProgress()
        //        {
        //            Id = e.Id,
        //            IdBeneficiary = e.IdBeneficiary,
        //            FirstName = beneficiary.FirstName,
        //            LastName = beneficiary.LastName,
        //            AccountCode = benef.AccountCode,
        //            IdUser = benef.IdUser,
        //        };
        //        effectivelist.Add(beneficiaryView);
        //    }
        //    return effectivelist;
        //}

    }
}

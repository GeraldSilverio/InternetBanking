using AutoMapper;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.CreditCards;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Services
{
    public class CreditsCardService : GenericService<CreditsCard, SaveCardViewModel, CardViewModel>, ICreditCardsService
    {
        
        public CreditsCardService(ICreditsCardRepository creditsCardRepository, IMapper mapper) : base(creditsCardRepository, mapper)
        {
        }

        public override Task<SaveCardViewModel> Add(SaveCardViewModel model)
        {
            model.CardNumber = GenerateCode.GenerateAccountCode(model);
            switch (model.SelectCard)
            {
                case nameof(CreditCards.CLASSIC): model.CreditLimited = (int)CreditCards.CLASSIC; break;
                case nameof(CreditCards.ELITE): model.CreditLimited = (int)CreditCards.ELITE; break;
                case nameof(CreditCards.GOLD): model.CreditLimited = (int)CreditCards.GOLD; break;
                case nameof(CreditCards.PLATINUM): model.CreditLimited = (int)CreditCards.PLATINUM; break;
                case nameof(CreditCards.DIAMOND): model.CreditLimited = (int)CreditCards.DIAMOND; break;
                case nameof(CreditCards.BLACK): model.CreditLimited = (int)CreditCards.BLACK; break;
            }
            model.Available = +model.CreditLimited;
            return base.Add(model);
        }
    }
}

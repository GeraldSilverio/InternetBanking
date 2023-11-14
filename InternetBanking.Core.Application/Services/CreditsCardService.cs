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
        private readonly ICreditsCardRepository _creditsCardRepository;
        private readonly IAccountService _accountService;
        public CreditsCardService(ICreditsCardRepository creditsCardRepository, IMapper mapper, IAccountService accountService) : base(creditsCardRepository, mapper)
        {
            _creditsCardRepository = creditsCardRepository;
            _accountService = accountService;
        }

        public override async Task<SaveCardViewModel> Add(SaveCardViewModel model)
        {
            model.CardNumber = GenerateCode.GenerateAccountCode(model.CurrentDate);
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
            return await base.Add(model);
        }

        public override async Task<List<CardViewModel>> GetAll()
        {
            var cardLists = new List<CardViewModel>();
            var cards  = await _creditsCardRepository.GetAllAsync();

            foreach (var card in cards)
            {
                var user = await _accountService.GetUserByIdAsync(card.IdUser);
                var cardViewModel = new CardViewModel()
                {
                    Id = card.Id,
                    IdUser = card.IdUser,
                    FullName = user.FirstName + " " + user.LastName,
                    IdentityCard = user.IdentityCard,
                    CardNumber = card.CardNumber,
                    CreditLimited = card.CreditLimited,
                    Available = card.Available,
                    Debt = card.CreditLimited - card.Available
                };
                cardLists.Add(cardViewModel);
            }

            return cardLists;
        }
    }
}

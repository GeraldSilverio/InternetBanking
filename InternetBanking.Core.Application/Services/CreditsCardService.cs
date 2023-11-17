using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.CreditCards;
using InternetBanking.Core.Application.ViewModels.Filter;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class CreditsCardService : GenericService<CreditsCard, SaveCardViewModel, CardViewModel>, ICreditCardsService
    {
        private readonly ICreditsCardRepository _creditsCardRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private AuthenticationResponse user;
        public CreditsCardService(ICreditsCardRepository creditsCardRepository, IMapper mapper, IAccountService accountService, IHttpContextAccessor httpContextAccessor) : base(creditsCardRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _creditsCardRepository = creditsCardRepository;
            _accountService = accountService;
            _mapper = mapper;
            user = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
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

        public async Task<List<CardViewModel>> GetAllWithFilters(FilterIdentityCardViewModel filters)
        {
            var cardLists = new List<CardViewModel>();
            var cards = await _creditsCardRepository.GetAllAsync();

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

            if (!string.IsNullOrEmpty(filters.IdentityCard))
            {
                string identityCardToLower = filters.IdentityCard.ToLower();
                cardLists = cardLists.Where(lr => lr.IdentityCard.ToLower().Contains(identityCardToLower)).ToList();
            }

            return cardLists;
        }

        public async Task<List<CardViewModel>> GetCreditCardsByUserId(string id)
        {
            var creditCards = await _creditsCardRepository.GetCreditCardsByUserIdAsync(id);
            return creditCards.Select(creditCard => new CardViewModel()
            {
                Id = creditCard.Id,
                CardNumber = creditCard.CardNumber,
                CreditLimited = creditCard.CreditLimited,
                Available = creditCard.Available,
                Debt = creditCard.Debt,
                FullName = user.FirstName + " " + user.LastName, 
                IdentityCard = user.IdentityCard
            }).ToList();
        }
    }
}

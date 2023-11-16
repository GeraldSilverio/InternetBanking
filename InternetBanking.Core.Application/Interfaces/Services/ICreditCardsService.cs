using InternetBanking.Core.Application.ViewModels.CreditCards;
using InternetBanking.Core.Application.ViewModels.Filter;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ICreditCardsService:IGenericService<CreditsCard, SaveCardViewModel, CardViewModel>
    {
        Task<List<CardViewModel>> GetAllWithFilters(FilterIdentityCardViewModel filters);
    }
}

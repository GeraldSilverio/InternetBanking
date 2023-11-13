using InternetBanking.Core.Application.ViewModels.CreditCards;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ICreditCardsService:IGenericService<CreditsCard, SaveCardViewModel, CardViewModel>
    {
    }
}

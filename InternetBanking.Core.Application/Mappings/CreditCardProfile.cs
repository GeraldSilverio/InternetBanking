using AutoMapper;
using InternetBanking.Core.Application.ViewModels.CreditCards;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Mappings
{
    public class CreditCardProfile : Profile
    {
        public CreditCardProfile()
        {
            CreateMap<CreditsCard, SaveCardViewModel>()
               .ReverseMap();
        }
    }
}

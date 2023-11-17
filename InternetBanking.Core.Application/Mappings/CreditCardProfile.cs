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
               .ReverseMap()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<CreditsCard, CardViewModel>()
               .ReverseMap();

            CreateMap<CardViewModel, SaveCardViewModel>()             
              .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(x => x.IdUser, opt => opt.MapFrom(src => src.IdUser))
              .ForMember(x => x.SelectCard, opt => opt.MapFrom(src => src.TypeOfCard))              
              .ReverseMap();
        }
    }
}

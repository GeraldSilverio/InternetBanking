using AutoMapper;
using InternetBanking.Core.Application.ViewModels.CreditCards;
using InternetBanking.Core.Application.ViewModels.Payment.EffectiveProgress;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Mappings
{
    public class EffectiveProgressProfile : Profile
    {
        public EffectiveProgressProfile()
        {
            CreateMap<EffectiveProgress, SaveEffectiveProgressViewModel>()     
            .ForMember(x => x.Error, opt => opt.Ignore())
            .ForMember(x => x.HasError, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<SaveEffectiveProgressViewModel, SaveCardViewModel>()
                .ForMember(x => x.CardNumber, opt => opt.MapFrom(src => src.OriginAccount));
           
        }
    }
}

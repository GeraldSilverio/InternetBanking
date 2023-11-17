using AutoMapper;
using InternetBanking.Core.Application.ViewModels.SavingAccount;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Mappings
{
    public class SavingAccountProfile : Profile
    {
        public SavingAccountProfile()
        {
            CreateMap<SavingAccount, CreateSavingAccountViewModel>()
                .ReverseMap();

            CreateMap<SavingAccount, SavingAccountViewModel>()
               .ReverseMap()
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
        }
    }
}

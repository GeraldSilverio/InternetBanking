using AutoMapper;
using InternetBanking.Core.Application.ViewModels.MoneyLoan;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Mappings
{
    public class MoneyLoanProfile : Profile
    {
        public MoneyLoanProfile()
        {
            CreateMap<MoneyLoan, NewMoneyLoanViewModel>()
             .ForMember(x => x.CurrentDate, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.Created, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
            CreateMap<MoneyLoan,MoneyLoanViewModel>();
        }
    }
}

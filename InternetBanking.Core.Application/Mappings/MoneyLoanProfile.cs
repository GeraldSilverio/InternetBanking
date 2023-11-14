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
                .ReverseMap()
                .ForMember(x => x.BalancePaid, opt => opt.Ignore());
            CreateMap<MoneyLoan,MoneyLoanViewModel>();
        }
    }
}

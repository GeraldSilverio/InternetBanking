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
        }
    }
}

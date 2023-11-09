using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Login;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(u => u.HasError, opt => opt.Ignore())
                .ForMember(u => u.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(u => u.HasError, opt => opt.Ignore())
                .ForMember(u => u.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AuthenticationResponse, UsersViewModel>();

            CreateMap<RegisterResponse, AuthenticationResponse>();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();

        }
    }
}

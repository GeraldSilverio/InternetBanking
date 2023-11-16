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

            #region User Profile

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(u => u.HasError, opt => opt.Ignore())
                .ForMember(u => u.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateUserRequest, EditUserViewModel>()
              .ForMember(u => u.HasError, opt => opt.Ignore())
              .ForMember(u => u.Error, opt => opt.Ignore())
              .ReverseMap();

            CreateMap<RegisterResponse, AuthenticationResponse>();

            CreateMap<AuthenticationResponse, UsersViewModel>();

            CreateMap<AuthenticationResponse, UserStatusViewModel>()
                .ReverseMap();

            CreateMap<EditUserViewModel, AuthenticationResponse>()
                .ReverseMap();



            #endregion

            #region Login Profile

            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(u => u.HasError, opt => opt.Ignore())
                .ForMember(u => u.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
              .ForMember(x => x.Error, opt => opt.Ignore())
              .ForMember(x => x.HasError, opt => opt.Ignore())
              .ReverseMap();

            #endregion
        }
    }
}

﻿using AutoMapper;
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

            CreateMap<AuthenticationResponse, UserStatusViewModel>()
                .ReverseMap();
                //.ForMember(u => u.UserName, opt => opt.Ignore())
                //.ForMember(u => u.FirstName, opt => opt.Ignore())
                //.ForMember(u => u.LastName, opt => opt.Ignore())
                //.ForMember(u => u.Email, opt => opt.Ignore())
                //.ForMember(u => u.IdentityCard, opt => opt.Ignore())
                //.ForMember(u => u.Roles, opt => opt.Ignore())
                //.ForMember(u => u.IsVerified, opt => opt.Ignore())
                //.ForMember(u => u.HasError, opt => opt.Ignore())
                //.ForMember(u => u.Error, opt => opt.Ignore());

            CreateMap<RegisterResponse, AuthenticationResponse>();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
              .ForMember(x => x.Error, opt => opt.Ignore())
              .ForMember(x => x.HasError, opt => opt.Ignore())
              .ReverseMap();

        }
    }
}

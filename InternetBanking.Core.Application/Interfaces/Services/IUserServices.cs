﻿using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Login;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IUserServices
    {
        Task<RegisterResponse> AddAsync(SaveUserViewModel viewModel, string origin);
        Task<List<UsersViewModel>> GetAllAsync();
        Task<ResetPasswordResponse> ChangePassword(ResetPasswordViewModel model);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model, string origin);
    }
}
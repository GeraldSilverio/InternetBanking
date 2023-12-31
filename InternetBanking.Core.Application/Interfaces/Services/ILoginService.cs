﻿
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Login;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface ILoginService
    {
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel saveViewModel, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel viewModel);
        Task<ResetPasswordResponse> ResetAsync(ResetPasswordViewModel viewModel);
        Task<string> ConfirmEmailAsync(string userId, string origin);
        Task SignOutAsync();
        List<AuthenticationResponse> GetAllUsers();
    }
}

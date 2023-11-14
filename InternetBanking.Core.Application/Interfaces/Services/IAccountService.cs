using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        List<AuthenticationResponse> GetAllUsersAsync();
        Task SignOutAsync();
        Task UpdateStatusAsync(string id, bool status);
        Task UpdateUserAsync(EditUserViewModel vm, string id);
        Task<AuthenticationResponse> GetUserByIdAsync(string id);
        
    }
}

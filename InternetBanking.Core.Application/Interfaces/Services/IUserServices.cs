using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.ViewModels.Login;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Interfaces.Services
{
    public interface IUserServices
    {
        Task<RegisterResponse> AddAsync(SaveUserViewModel viewModel, string origin);
        List<UsersViewModel> GetAllAsync();
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel model);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model, string origin);
        Task UpdateStatus(string id, bool status);
        Task UpdateUser(EditUserViewModel vm, string id);
        UserStatusViewModel GetUserById(string id);
        EditUserViewModel GetUserViewModelById(string id);
    }
}

using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Login;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public UserServices(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<RegisterResponse> AddAsync(SaveUserViewModel viewModel, string origin)
        {
            var request = _mapper.Map<RegisterRequest>(viewModel);
            return  await _accountService.RegisterBasicUserAsync(request, origin);
        }

        public Task<ResetPasswordResponse> ChangePassword(ResetPasswordViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model, string origin)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UsersViewModel>> GetAllAsync()
        {
            var request = await _accountService.GetAllUsersAsync();
            var user = _mapper.Map<List<UsersViewModel>>(request);
            return user;
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            ResetPasswordRequest forgotRequest = _mapper.Map<ResetPasswordRequest>(model);
            return await _accountService.ResetPasswordAsync(forgotRequest);
        }
    }
}

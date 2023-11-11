using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Login;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public LoginService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }        
        public async Task<RegisterResponse> RegisterAsync(SaveUserViewModel saveViewModel, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(saveViewModel);
            return await _accountService.RegisterBasicUserAsync(registerRequest,origin);
        }

        public async Task<ResetPasswordResponse> ResetAsync(ResetPasswordViewModel viewModel)
        {
            ResetPasswordRequest resetPasswordRequest = _mapper.Map<ResetPasswordRequest>(viewModel);
            return await _accountService.ResetPasswordAsync(resetPasswordRequest);
        }

        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel viewModel)
        {
            AuthenticationRequest authenticationRequest = _mapper.Map<AuthenticationRequest>(viewModel);
            AuthenticationResponse authenticationResponse = await _accountService.AuthenticateAsync(authenticationRequest);
            return authenticationResponse;
        }

        public async Task<string> ConfirmEmailAsync(string userId, string origin)
        {
            return await _accountService.ConfirmAccountAsync(userId, origin);
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public List<AuthenticationResponse> GetAllUsers()
        {
            return _accountService.GetAllUsersAsync();
        }
    }
}

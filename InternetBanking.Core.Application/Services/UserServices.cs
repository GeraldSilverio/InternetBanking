using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Login;
using InternetBanking.Core.Application.ViewModels.SavingAccount;
using InternetBanking.Core.Application.ViewModels.User;

namespace InternetBanking.Core.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IAccountService _accountService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IMapper _mapper;

        public UserServices(IAccountService accountService, IMapper mapper, ISavingAccountService savingAccountService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _savingAccountService = savingAccountService;
        }

        public async Task<RegisterResponse> AddAsync(SaveUserViewModel viewModel, string origin)
        {
            var request = _mapper.Map<RegisterRequest>(viewModel);

            var response = await _accountService.RegisterBasicUserAsync(request, origin);
            //Creando la cuenta del usuario.
            if (!response.HasError && viewModel.SelectRole == (int)Roles.Client)
            {
                var savingAccount = new CreateSavingAccountViewModel()
                {
                    AccountCode = 923222234,
                    IdUser = response.IdUser,
                    Balance = viewModel.BalanceAccount,
                    IsPrincipal = false
                };
                await _savingAccountService.Add(savingAccount);
            }

            return response;
        }

        public Task<ResetPasswordResponse> ChangePassword(ResetPasswordViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(model);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
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

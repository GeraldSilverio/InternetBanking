using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Payment.Transaction;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class TransactionService : GenericService<Transaction, SaveTransactionViewModel, TransactionViewModel>, ITransactionService
    {
        private readonly ISavingAccountService _savingAccountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private AuthenticationResponse User;
        private readonly IMapper _mapper;
        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, ISavingAccountService savingAccountService,
            IHttpContextAccessor httpContextAccessor) : base(transactionRepository, mapper)
        {
            _savingAccountService = savingAccountService;
            _httpContextAccessor = httpContextAccessor;
            User = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<SaveTransactionViewModel> AddTransaction(SaveTransactionViewModel model)
        {
            try
            {
                model.IdUser = User.Id;
                var originAccount = await _savingAccountService.GetById(model.OriginAccount);
                var destinationAccount = await _savingAccountService.GetById(model.DestinationAccount);

                if (originAccount.Balance < model.Amount)
                {
                    model.HasError = true;
                    model.Error = "LA CUENTA DE AHORRO SELECCIONADA NO POSEE EL DINERO SUFICIENTE PARA ESTE DEPOSITO";
                    return model;
                }

                if (originAccount.Id == destinationAccount.Id)
                {
                    model.HasError = true;
                    model.Error = "NO PUEDE REALIZAR UN DEPOSITO A LA MISMA CUENTA";
                    return model;
                }

                originAccount.Balance -= model.Amount;             
                destinationAccount.Balance += model.Amount;

                await _savingAccountService.Update(originAccount, originAccount.Id);
                await _savingAccountService.Update(destinationAccount, destinationAccount.Id);

                return await base.Add(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}

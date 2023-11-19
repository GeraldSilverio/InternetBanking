using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.CreditCards;
using InternetBanking.Core.Application.ViewModels.Payment.EffectiveProgress;
using InternetBanking.Core.Application.ViewModels.SavingAccount;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class EffectiveProgressService : GenericService<EffectiveProgress, SaveEffectiveProgressViewModel, EffectiveProgress> , IEffectiveProgressService
    {
        private readonly ICreditCardsService _creditCardsService;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private AuthenticationResponse User;
        private readonly IMapper _mapper;
        public EffectiveProgressService
            (IEffectiveProgressRepository effectiveProgressRepository, IMapper mapper, ICreditCardsService creditCardsService, ISavingAccountService savingAccountService,
            IHttpContextAccessor httpContextAccessor)
            : base(effectiveProgressRepository,mapper)
        {            
            _creditCardsService = creditCardsService;
            _savingAccountService = savingAccountService;
            _httpContextAccessor = httpContextAccessor;
            User = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<SaveEffectiveProgressViewModel> AddEffectiveProgress(SaveEffectiveProgressViewModel model)
        {
            try
            {
                model.IdUser = User.Id;
                var originAccount = await _creditCardsService.GetById(model.OriginAccount);
                var destinationAccount = await _savingAccountService.GetById(model.DestinationAccount);

                if (model.Amount > originAccount.Available)
                {
                    model.HasError = true;
                    model.Error = "EL MONTO INGRESADO EXCEDE EL LÍMITE DE LA TARJETA SELECCIONADA";

                    return model;
                }

                //Agregar una validacion que si el usuario manda el monto en 0 decirle que el monto debe ser mayor a 100 por ejemplo
                decimal deuda = model.Amount + (model.Amount * 0.0625m);
                originAccount.Debt += deuda;
                originAccount.Available = originAccount.Available - deuda;
                destinationAccount.Balance += model.Amount;

                await _creditCardsService.Update(originAccount,originAccount.Id);
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

using AutoMapper;
using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Beneficiary;
using InternetBanking.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Core.Application.Services
{
    public class BeneficiaryService : GenericService<Beneficiary, SaveBeneficiaryViewModel, BeneficiaryViewModel>, IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISavingAccountService _savingAccountService;
        private readonly IMapper _mapper;
        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IAccountService accountService, IMapper mapper, IHttpContextAccessor httpContextAccessor, ISavingAccountService savingAccountService) : base(beneficiaryRepository, mapper)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _savingAccountService = savingAccountService;
            _mapper = mapper;
        }

        public override async Task<SaveBeneficiaryViewModel> Add(SaveBeneficiaryViewModel model)
        {
            var User = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            var beneficiaryAccount = await _savingAccountService.GetByAccountCode(model.AccountCode);

            //Validando que el usuario exista.
            if (beneficiaryAccount is null)
            {
                model.HasError = true;
                model.Error = "ESTA CUENTA BANCARIA NO EXISTE";
                return model;
            }
            //Validando que ya no lo tenga agregado como beneficiario.
            var areBeneficiary = await IsBeneficiaryAdd(User.Id, beneficiaryAccount.IdUser, model.AccountCode);

            if (areBeneficiary is true)
            {
                model.HasError = true;
                model.Error = $"YA TIENES AGREGADO A ESTE USUARIO CON EL NUMERO DE CUENTA{model.AccountCode},POR FAVOR INTENTA CON OTRA CUENTA";
                return model;
            }
            //Validando que no se quiere agregar el mismo.
            if (User.Id == beneficiaryAccount.IdUser)
            {
                model.HasError = true;
                model.Error = "NO PUEDES AGREGARTE A TI MISMO COMO BENEFICIARIO";
                return model;
            }
            model.IdUser = User.Id;
            model.IdBeneficiary = beneficiaryAccount.IdUser;
            return await base.Add(model);
        }

        public async Task<bool> IsBeneficiaryAdd(string idUser, string idBeneficiary, int accountCode)
        {
            return await _beneficiaryRepository.IsBeneficiaryAdd(idUser, idBeneficiary, accountCode);
        }

        public async Task<List<BeneficiaryViewModel>> GetAllByUser(string id)
        {
            var benefiaciresList = new List<BeneficiaryViewModel>();
            var beneficiaries = await _beneficiaryRepository.GetAllByUser(id);
            foreach (var benef in beneficiaries)
            {
                var beneficiary = await _accountService.GetUserByIdAsync(benef.IdBeneficiary);
                var beneficiaryView = new BeneficiaryViewModel()
                {
                    Id = benef.Id,
                    IdBeneficiary = benef.IdBeneficiary,
                    FirstName = beneficiary.FirstName,
                    LastName = beneficiary.LastName,
                    AccountCode = benef.AccountCode,
                    IdUser = benef.IdUser,
                };
                benefiaciresList.Add(beneficiaryView);
            }
            return benefiaciresList;
        }

        public async Task<BeneficiaryViewModel> GetByAccountCode(int accountCode)
        {
            return _mapper.Map<BeneficiaryViewModel>(await _beneficiaryRepository.GetByAccountCode(accountCode));
        }
    }
}

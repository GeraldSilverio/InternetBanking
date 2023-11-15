using AutoMapper;
using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.Beneficiary;
using InternetBanking.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.Services
{
    public class BeneficiaryService : GenericService<Beneficiary, SaveBeneficiaryViewModel, BeneficiaryViewModel>, IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IMapper mapper, IAccountService accountService) : base(beneficiaryRepository, mapper)
        {
            _beneficiaryRepository = beneficiaryRepository;
            _accountService = accountService;
            _mapper = mapper;
        }

        public override async Task<SaveBeneficiaryViewModel> Add(SaveBeneficiaryViewModel model)
        {
            var user = await _accountService.GetUserByIdAsync(model.IdUser);
            var beneficiary = await _accountService.GetUserByIdAsync(model.IdUser);

            //Validando que el usuario exista.
            if (beneficiary == null)
            {
                model.HasError = true;
                model.Error = "ESTE USUARIO NO EXISTE";
                return model;
            }
            //Validando que no sean amigos.
            var areFriend = await IsBeneficiaryAdd(model.IdUser, beneficiary.Id);

            if (areFriend == false)
            {
                model.HasError = true;
                model.Error = "ESTE USUARIO YA ES TU BENEFICIARIO, NO LO PUEDES VOLVER A AGREGAR";
                return model;
            }
            //Validando que no se quiere agregar el mismo.
            if (model.IdUser == user.Id)
            {
                model.HasError = true;
                model.Error = "NO PUEDES AGREGARTE A TI MISMO COMO BENEFICIARIO";
                return model;
            }
            model.IdBeneficiary = beneficiary.Id;


            return await base.Add(model);
        }

        public async Task<bool> IsBeneficiaryAdd(string idUser, string idBeneficiary)
        {
            return await _beneficiaryRepository.IsBeneficiaryAdd(idUser, idBeneficiary);
        }
    }
}

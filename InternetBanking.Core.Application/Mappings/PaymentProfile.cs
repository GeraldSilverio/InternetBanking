using AutoMapper;
using InternetBanking.Core.Application.ViewModels.Payment;
using InternetBanking.Core.Domain.Entities;

namespace InternetBanking.Core.Application.Mappings
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment, SavePaymentViewModel>()
                .ForMember(x => x.FullName, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

        }
    }
}

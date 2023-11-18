using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Services;
using InternetBanking.Core.Application.Services.ValidatePayment;
using InternetBanking.Core.Application.Strategy.Context;
using InternetBanking.Core.Application.Strategy.Interfaces;
using InternetBanking.Core.Application.Strategy.Methods;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InternetBanking.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<ISavingAccountService, SavingAccountService>();
            services.AddTransient<ICreditCardsService, CreditsCardService>();
            services.AddTransient<ICreditCardsService, CreditsCardService>();
            services.AddTransient<IMoneyLoanService, MoneyLoanService>();
            services.AddTransient<IGetCountProduct, GetCountProducts>();
            services.AddTransient<IBeneficiaryService, BeneficiaryService>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IEffectiveProgressService, EffectiveProgressService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IValidatePayment, ValidatePayment>();
            #endregion

            #region Strategy
            services.AddTransient<IPayment, MoneyLoanPayment>();
            services.AddTransient<IPayment, CreditCardPayment>();
            services.AddTransient<PaymentContext>();
            #endregion
        }
    }
}
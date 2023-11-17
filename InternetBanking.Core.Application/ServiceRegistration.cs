using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.Services;
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
            #endregion
        }
    }
}
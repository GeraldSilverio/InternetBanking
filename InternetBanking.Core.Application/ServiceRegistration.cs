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
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<ISavingAccountService, SavingAccountService>();
            services.AddScoped<ICreditCardsService, CreditsCardService>();
            services.AddScoped<ICreditCardsService, CreditsCardService>();
            services.AddScoped<IMoneyLoanService, MoneyLoanService>();
            services.AddScoped<IGetCountProduct, GetCountProducts>();
            #endregion
        }
    }
}
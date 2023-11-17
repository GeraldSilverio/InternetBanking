using InternetBanking.Core.Application.Interfaces.Repositories;
using InternetBanking.Infraestructure.Persistence.Contexts;
using InternetBanking.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBanking.Infraestructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region DbContext
            if (configuration.GetValue<bool>("UseInMemoryDataBase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)),
                ServiceLifetime.Scoped);
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IBeneficiaryRepository, BeneficiaryRepository>();
            services.AddTransient<ICreditsCardRepository, CreditsCardRepository>();
            services.AddTransient<IMoneyLoanRepository, MoneyLoanRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<ISavingAccountRepository, SavingAccountRepository>();
            services.AddTransient<IEffectiveProgressRepository, EffectiveProgressRepository>();
            #endregion
        }
    }
}
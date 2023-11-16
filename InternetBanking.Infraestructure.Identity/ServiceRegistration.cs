using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Infraestructure.Identity.Contexts;
using InternetBanking.Infraestructure.Identity.Entities;
using InternetBanking.Infraestructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBanking.Infraestructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfraestructure(this IServiceCollection services,IConfiguration  configuration)
        {
            #region IdentityContext
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
            }
            else
            {
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });
            }
            #endregion

            #region Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Login/AccessDenied";
            });

            services.AddAuthentication();

            #endregion

            #region Identity Service
            services.AddTransient<IAccountService, AccountService>();
            #endregion
        }
    }
}
﻿using InternetBanking.Core.Application.Interfaces.Services;
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
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<ISavingAccountService, SavingAccountService>();
            services.AddScoped<ICreditCardsService, CreditsCardService>();
            #endregion
        }
    }
}
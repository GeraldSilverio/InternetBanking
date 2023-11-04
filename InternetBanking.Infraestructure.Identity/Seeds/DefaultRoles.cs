using InternetBanking.Core.Application.Enums;
using InternetBanking.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infraestructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Client.ToString()));
        }
    }
}

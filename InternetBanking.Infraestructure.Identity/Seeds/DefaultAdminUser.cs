using InternetBanking.Core.Application.Enums;
using InternetBanking.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infraestructure.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new()
            {
                UserName = "adminuser",
                Email = "adminuser@gmail.com",
                FirstName = "Usuario",
                LastName = "Admin",                
                IdentityCard = "406-6560680-6",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive =  true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {                
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }

        }
    }
}

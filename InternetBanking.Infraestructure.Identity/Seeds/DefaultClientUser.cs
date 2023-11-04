using InternetBanking.Core.Application.Enums;
using InternetBanking.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Infraestructure.Identity.Seeds
{
    public static class DefaultClientUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultUser = new()
            {
                UserName = "clientuser",
                Email = "clientuser@gmail.com",
                FirstName = "Usuario",
                LastName = "Cliente",
                IdentityCard = "406-6560680-6",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123ClientC#.");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Client.ToString());                    
                }
            }

        }
    }
}

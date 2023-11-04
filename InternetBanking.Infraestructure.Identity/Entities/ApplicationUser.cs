﻿using Microsoft.AspNetCore.Identity;

namespace InternetBanking.Infraestructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityCard { get ; set; }
    }
}

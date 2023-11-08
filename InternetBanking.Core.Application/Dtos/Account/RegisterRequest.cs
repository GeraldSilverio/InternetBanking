﻿namespace InternetBanking.Core.Application.Dtos.Account
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string IdentityCard { get; set; }
        public bool IsActive { get; set; }
        public string SelectRole {  get; set; }

    }
}

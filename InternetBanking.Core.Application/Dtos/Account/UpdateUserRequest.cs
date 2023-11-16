﻿namespace InternetBanking.Core.Application.Dtos.Account
{
    public class UpdateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IdentityCard { get; set; }
    }
}

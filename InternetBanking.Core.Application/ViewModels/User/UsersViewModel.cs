namespace InternetBanking.Core.Application.ViewModels.User
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string IdentityCard { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}

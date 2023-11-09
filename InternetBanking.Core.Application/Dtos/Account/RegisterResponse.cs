namespace InternetBanking.Core.Application.Dtos.Account
{
    public class RegisterResponse
    {
        public string? IdUser {  get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}

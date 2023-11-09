namespace InternetBanking.Core.Application.Dtos.Account
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
    }
}

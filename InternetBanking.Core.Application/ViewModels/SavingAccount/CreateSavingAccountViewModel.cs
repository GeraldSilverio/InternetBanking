namespace InternetBanking.Core.Application.ViewModels.SavingAccount
{
    public class CreateSavingAccountViewModel
    {
        public int AccountCode { get; set; }
        public string IdUser { get; set; }
        public decimal Balance { get; set; }
        public bool IsPrincipal { get; set; }
    }
}

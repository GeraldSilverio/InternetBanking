namespace InternetBanking.Core.Application.ViewModels.SavingAccount
{
    public class SavingAccountViewModel
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string FullName { get; set; }
        public string IdentityCard { get; set; }        
        public int AccountCode { get; set; }
        public decimal Balance { get; set; }
        public bool IsPrincial { get; set; }
    }
}

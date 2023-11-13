namespace InternetBanking.Core.Application.ViewModels.CreditCards
{
    public class CardViewModel
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string FullName { get; set; }
        public string IdentityCard { get; set; }
        public int CardNumber { get; set; }
        public decimal CreditLimited { get; set; }
        public decimal Available { get; set; }
        public decimal Spent { get; set; }
        public decimal Debt { get; set; }
    }
}

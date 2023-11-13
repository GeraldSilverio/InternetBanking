namespace InternetBanking.Core.Application.ViewModels.CreditCards
{
    public class SaveCardViewModel:DefaultViewModel
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string SelectCard { get; set; }
        public int CardNumber { get; set; }
        public decimal CreditLimited { get; set; }
        public decimal Available { get; set; }
    }
}

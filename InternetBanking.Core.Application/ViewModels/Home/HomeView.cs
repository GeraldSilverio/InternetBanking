namespace InternetBanking.Core.Application.ViewModels.Home
{
    public class HomeView
    {
        public int ActiveUser { get; set; }
        public int InActiveUser { get; set; }
        public int Products { get; set; }
        public int Payments { get; set; }
        public int PaymentsPerDay { get; set; }
        public int Transactions { get; set; }
        public int TransactionsPerDay { get; set; }
    }
}

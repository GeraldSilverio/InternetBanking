namespace InternetBanking.Core.Application.ViewModels.Transactions
{
    public class SaveTransactionViewModel
    {
        public decimal Aumont { get; set; }
        public string IdUser { get; set; }
        public DateTime DateOfPaid { get; set; }
        public int OriginAccount { get; set; }
        public int DestinationAccount { get; set; }
    }
}

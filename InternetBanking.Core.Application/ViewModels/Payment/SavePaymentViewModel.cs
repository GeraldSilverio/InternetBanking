namespace InternetBanking.Core.Application.ViewModels.Payment
{
    public class SavePaymentViewModel
    {
        public int Id { get; set; }
        public string? IdUser { get; set; }
        public int OriginAccount { get; set; }
        public int DestinationAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfPaid { get; set; } = DateTime.Now;
        public string? Error { get; set; }
        public bool HasError { get; set; }
        public string? FullName { get; set; }
    }
}

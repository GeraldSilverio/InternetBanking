
namespace InternetBanking.Core.Application.ViewModels.Payment.EffectiveProgress
{
    public class SaveEffectiveProgressViewModel
    {
        public int Id { get; set; }
        public string? IdUser { get; set; }
        public int OriginAccount { get; set; }
        public int DestinationAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateOfPaid { get; set; } = DateTime.Now;
        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}

using InternetBanking.Core.Domain.Commons;

namespace InternetBanking.Core.Domain.Entities
{
    public class Payment : AuditableBaseEntity
    {
        public string IdUser { get; set; } = null!;
        public int OriginAccount { get; set; }
        public int DestinationAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string TypeOfPayment { get; set; } = null!;
    }
}

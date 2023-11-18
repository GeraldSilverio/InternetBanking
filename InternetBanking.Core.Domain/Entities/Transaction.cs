using InternetBanking.Core.Domain.Commons;

namespace InternetBanking.Core.Domain.Entities
{
    public class Transaction : AuditableBaseEntity
    {
        public string IdUser { get; set; }
        public int OriginAccount { get; set; }
        public int DestinationAccount { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}

using InternetBanking.Core.Domain.Commons;

namespace InternetBanking.Core.Domain.Entities
{
    public class CreditsCard:AuditableBaseEntity
    {
        public string IdUser { get; set; } = null!;
        public int CardNumber { get; set; }
        public decimal CreditLimited { get; set; }  
        public decimal Available { get; set; }
        public decimal Debt { get ; set; }
    }
}

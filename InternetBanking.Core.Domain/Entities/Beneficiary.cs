using InternetBanking.Core.Domain.Commons;

namespace InternetBanking.Core.Domain.Entities
{
    public class Beneficiary:AuditableBaseEntity
    {
        public string IdUser { get; set; } = null!;
        public string IdBeneficiary { get; set; } = null!;
        public int AccountCode { get; set; }
    }
}

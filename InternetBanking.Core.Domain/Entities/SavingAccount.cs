using InternetBanking.Core.Domain.Commons;

namespace InternetBanking.Core.Domain.Entities
{
    public class SavingAccount : AuditableBaseEntity
    {
        public int AccountCode { get; set; }
        public string IdUser { get; set; } = null!;
        public decimal Balance { get; set; }
        public bool IsPrincipal { get; set; }

    }
}

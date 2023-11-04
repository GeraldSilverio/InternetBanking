using InternetBanking.Core.Domain.Commons;

namespace InternetBanking.Core.Domain.Entities
{
    public class CreditsCard:AuditableBaseEntity
    {
        public string IdUser { get; set; } = null!;
        public int CardNumber { get; set; }
        //Limite de la tarjeta
        public decimal CreditLimited { get; set; }
        //Balance disponible
        public decimal Available { get; set; }
        //Lo que ha gastado
        public decimal Spent { get; set; }
    }
}

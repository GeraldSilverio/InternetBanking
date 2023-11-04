using InternetBanking.Core.Domain.Commons;

namespace InternetBanking.Core.Domain.Entities
{
    public class MoneyLoan:AuditableBaseEntity
    {
        public string IdUser { get; set; } = null!;
        public int MoneyLoanCode { get; set; }
        public decimal BalancePaid {  get; set; }
        public decimal BorrowedBalance{  get; set; }




    }
}

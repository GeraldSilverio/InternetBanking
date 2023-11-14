namespace InternetBanking.Core.Application.ViewModels.MoneyLoan
{
    public class MoneyLoanViewModel
    {
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string UserFullName { get; set; }
        public string UserIdentityCard { get; set; }
        public int MoneyLoanCode { get; set; }
        public decimal BalancePaid { get; set; }
        public decimal BorrowedBalance { get; set; }
        public decimal Debt { get; set; }
    }
}

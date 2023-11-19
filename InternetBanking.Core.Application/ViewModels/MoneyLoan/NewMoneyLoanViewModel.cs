using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.MoneyLoan
{
    public class NewMoneyLoanViewModel
    {
        public int Id {  get; set; }
        public string IdUser {  get; set; }
        public int MoneyLoanCode {  get; set; }
        public decimal BorrowedBalance {  get; set; }
        public decimal BalancePaid {  get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.SavingAccount
{
    public class CreateSavingAccountViewModel
    {
        public int Id { get; set; }
        public int AccountCode { get; set; } = 1;
        [Required]
        public string? IdUser { get; set; }
        [Required]
        public decimal Balance { get; set; }
        public bool? IsPrincipal { get; set; } = false;
        public DateTime CurrentDate { get; set; } = DateTime.Now;
    }
}

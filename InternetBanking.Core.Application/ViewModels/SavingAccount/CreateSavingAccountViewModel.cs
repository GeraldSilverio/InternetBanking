using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.SavingAccount
{
    public class CreateSavingAccountViewModel:DefaultViewModel
    {
        public int AccountCode { get; set; } = 1;
        [Required]
        public string? IdUser { get; set; }
        [Required]
        public decimal? Balance { get; set; }
        public bool? IsPrincipal { get; set; } = false;
    }
}

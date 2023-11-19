using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Beneficiary
{
    public class SaveBeneficiaryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Este campo es requerido")]
        public int AccountCode { get; set; }
        public string? IdUser { get; set; }
        public string? IdBeneficiary { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }

    }
}

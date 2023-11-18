
using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Payment.EffectiveProgress
{
    public class SaveEffectiveProgressViewModel
    {
        public int Id { get; set; }
        public string? IdUser { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Este campo es requerido")]
        public int OriginAccount { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "Este campo es requerido")]
        public int DestinationAccount { get; set; }
        [Range(1, (double)decimal.MaxValue, ErrorMessage = "Debe ingresar un monto")]
        public decimal Amount { get; set; }
        public DateTime DateOfPaid { get; set; } = DateTime.Now;
        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}

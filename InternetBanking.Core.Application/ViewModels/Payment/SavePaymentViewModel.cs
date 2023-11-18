using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Payment
{
    public class SavePaymentViewModel
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
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public string? Error { get; set; }
        public bool HasError { get; set; }
        public string? FullName { get; set; }
        public string? TypeOfPayment { get; set; }

    }
}

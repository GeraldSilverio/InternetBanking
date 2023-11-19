using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.Payment.Transaction
{
    public class SaveTransactionViewModel
    {
        public int Id { get; set; }
        public string? IdUser { get; set; }
        [Range(1, (double)decimal.MaxValue, ErrorMessage = "Debe seleccionar una cuenta")]
        public int OriginAccount { get; set; }

        [Range(1, (double)decimal.MaxValue, ErrorMessage = "Debe seleccionar una cuenta")]
        public int DestinationAccount { get; set; }

        [Range(1, (double)decimal.MaxValue, ErrorMessage = "Debe ingresar un monto")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.Now.Date;
        public string? Error { get; set; }
        public bool HasError { get; set; }
    }
}

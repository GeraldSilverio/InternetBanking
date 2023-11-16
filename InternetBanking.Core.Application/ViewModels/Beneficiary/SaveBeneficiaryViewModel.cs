using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Beneficiary
{
    public class SaveBeneficiaryViewModel
    {
        public int Id { get; set; }
        public int AccountCode { get; set; }
        public string? IdUser { get; set; }
        public string? IdBeneficiary { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }

    }
}

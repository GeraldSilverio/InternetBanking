using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Beneficiary
{
    public class BeneficiaryViewModel
    {
        public string IdUser { get; set; } = null!;
        public string IdBeneficiary { get; set; } = null!;
        public int AccountCode { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

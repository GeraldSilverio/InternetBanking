using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBanking.Core.Application.ViewModels.Client
{
    public class ClientViewModel
    {

        #region User Atributes
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string IdentityCard { get; set; }
        public List<string> Roles { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        #endregion

        #region CreditsCard Atributes
        public int CardNumber { get; set; }
        //Limite de la tarjeta
        public decimal CreditLimited { get; set; }
        //Balance disponible
        public decimal Available { get; set; }
        //Lo que ha gastado
        public decimal Spent { get; set; }
        #endregion

        #region MoneyLoan Atributes
        public int MoneyLoanCode { get; set; }
        public decimal BalancePaid { get; set; }
        public decimal BorrowedBalance { get; set; }
        #endregion

        #region SavingAccount Atributes
        public int AccountCode { get; set; }
        public decimal Balance { get; set; }
        public bool IsPrincipal { get; set; }
        #endregion

        #region Beneficiary
        public string IdBeneficiary { get; set; } = null!;
        #endregion

        public bool HasError { get; set; }
        public string? Error { get; set; }

    }
}

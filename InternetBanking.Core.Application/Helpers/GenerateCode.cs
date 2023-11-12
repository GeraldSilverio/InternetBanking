using InternetBanking.Core.Application.ViewModels;

namespace InternetBanking.Core.Application.Helpers
{
    public static class GenerateCode
    {
        public static int GenerateAccountCode(DefaultViewModel model)
        {

            string formattedDate = model.CurrentDate.ToString("MddHmss");

            if (int.TryParse(formattedDate, out int accountCode))
            {
                accountCode %= 1000000000;
                return accountCode;
            }
            else
            {
                throw new InvalidOperationException("No se pudo generar el código de cuenta");
            }
        }
    }
}

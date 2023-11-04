using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;

namespace WebApp.InternetBanking.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse authenticationResponse = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

            if(authenticationResponse == null)
            {
                return false;
            }
            return true;
        }
    }
}

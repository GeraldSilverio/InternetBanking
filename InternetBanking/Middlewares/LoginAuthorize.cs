using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.InternetBanking.Controllers;

namespace WebApp.InternetBanking.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _validateUserSession;
        public LoginAuthorize(ValidateUserSession validateUserSession)
        {
            _validateUserSession = validateUserSession;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {            
            if (_validateUserSession.HasUser())
            {
                var controller = (LoginController)context.Controller;

                context.Result = controller.RedirectToAction("Index", "Home");
            }
            else
            {
                await next();
            }
        }
    }
}

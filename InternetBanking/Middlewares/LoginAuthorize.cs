using InternetBanking.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

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
            //Aunque este puesto el home controller, no quiere decir que sea el controlador correspondiente
            if (_validateUserSession.HasUser())
            {
                var controller = (HomeController)context.Controller;
                context.Result = controller.RedirectToAction("index", "home");
            }
            else
            {
                await next();
            }
        }
    }
}

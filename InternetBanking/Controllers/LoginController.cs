using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using WebApp.InternetBanking.Middlewares;

namespace WebApp.InternetBanking.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        #region Login And Register
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View("Login", new LoginViewModel());
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", vm);
            }

            AuthenticationResponse authenticationResponse = await _userService.LoginAsync(vm);
            if (authenticationResponse != null && authenticationResponse.HasError == false)
            {
                HttpContext.Session.Set("user", authenticationResponse);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                vm.HasError = authenticationResponse.HasError;
                vm.Error = authenticationResponse.Error;
                return View("Login", vm);
            }
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel saveVM)
        {
            if (!ModelState.IsValid)
            {
                return View(saveVM);
            }
            var origin = Request.Headers["origin"];

            RegisterResponse response = await _userService.RegisterAsync(saveVM, origin);

            if (response.HasError)
            {
                saveVM.HasError = response.HasError;
                saveVM.Error = response.Error;
                return View(saveVM);
            }
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
        #endregion

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
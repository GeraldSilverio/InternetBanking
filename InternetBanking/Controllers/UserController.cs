using InternetBanking.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> UserAdministrator()
        {
            return View(await _userService.GetAllUsers());
        }
    }
}

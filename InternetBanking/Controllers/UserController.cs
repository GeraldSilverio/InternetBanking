﻿using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Helpers;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userService;

        public UserController(IUserServices userService)
        {
            _userService = userService;
        }
        //Este metodo solo entran admins.
        public async Task<IActionResult> UserAdministrator()
        {
            return View(await _userService.GetAllAsync());
        }
        //Este metodo solo entran admin
        public async Task<IActionResult> AddUser()
        {
            return View(new SaveUserViewModel());
        }

        //Este metodo solo entran admin
        [HttpPost]
        public async Task<IActionResult> AddUser(SaveUserViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                var origin = Request.Headers["origin"];

                RegisterResponse response = await _userService.AddAsync(viewModel, origin);

                if (response.HasError)
                {
                    viewModel.HasError = response.HasError;
                    viewModel.Error = response.Error;
                    return View(viewModel);
                }
                return RedirectToRoute(new { controller = "User", action = "UserAdministrator" });
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        public IActionResult ResetPassword()
        {
            return View(new ResetPasswordViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var response = await _userService.ResetPasswordAsync(model);
            if (response.HasError)
            {
                model.HasError = response.HasError;
                model.Error = response.Error;
                return View(model);
            }
            return RedirectToRoute(new { controller = "User", action = "PasswordConfirm" });
        }
    }
}

using InternetBanking.Core.Application.Dtos.Account;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.InternetBanking.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserServices _userService;
        private readonly IMoneyLoanService _moneyLoanService;

        public UserController(IUserServices userService, IMoneyLoanService moneyLoanService)
        {
            _userService = userService;
            _moneyLoanService = moneyLoanService;
        }
        public IActionResult UserAdministrator()
        {
            return View(_userService.GetAllAsync());
        }
        public  IActionResult AddUser()
        {
            return View(new SaveUserViewModel());
        }

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

        public async Task<IActionResult> EditUser(string id)
        {
            EditUserViewModel editUser =  await _userService.GetUserViewModelById(id);
            return View("EditUser", editUser);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel vm)
        {

            //if (!ModelState.IsValid)
            //{
            //    EditUserViewModel editUser = await _userService.GetUserViewModelById(vm.Id);
            //    ViewBag.NewMoneyLoan = await _moneyLoanService.GetMoneyLoansByUserId(editUser.Id);
            //    return View("EditUser", editUser);
            //}

            //if (vm.ErrorMessage)
            //{
            //    vm.ErrorMessage = true;
            //    vm.Error = "El monto del prestamo debe ser mayor $RD 100";
            //}

            await _userService.UpdateUser(vm, vm.Id);
            return RedirectToAction("UserAdministrator");


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

        public async Task<ActionResult> DesactivateUser(string id)
        {
            UserStatusViewModel viewModel =  await _userService.GetUserById(id);
            return View("ActiveOrDesactiveUser", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DesactivateUser(UserStatusViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                await _userService.UpdateStatus(vm.Id, false);
            }
            catch (Exception)
            {

                throw;
            }


            return RedirectToAction("UserAdministrator");
        }

        public async Task<IActionResult> ActivateUser(string id)
        {
            UserStatusViewModel viewModel =  await _userService.GetUserById(id);
            return View("ActiveOrDesactiveUser", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ActivateUser(UserStatusViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            try
            {
                
                await _userService.UpdateStatus(vm.Id, true);
            }
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction("UserAdministrator");
        }
    }
}

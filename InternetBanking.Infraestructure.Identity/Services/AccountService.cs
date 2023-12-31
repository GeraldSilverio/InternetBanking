﻿using InternetBanking.Core.Application.Enums;
using InternetBanking.Core.Application.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using InternetBanking.Infraestructure.Identity.Entities;
using InternetBanking.Core.Application.Dtos.Email;
using InternetBanking.Core.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using InternetBanking.Infraestructure.Identity.Contexts;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace InternetBanking.Infraestructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        public readonly IdentityContext _identityContext;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IdentityContext identityContext, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _identityContext = identityContext;
        }

        #region Register User
        public async Task<RegisterResponse> RegisterBasicUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {

                HasError = false
            };

            var userEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userEmail != null)
            {
                response.HasError = true;
                response.Error = $"El correo '{request.Email}' ya esta en uso";
                return response;
            }

            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.Phone,
                IdentityCard = request.IdentityCard,
                IsActive = false,
                EmailConfirmed = true

            };

            var result = await _userManager.CreateAsync(user, request.Password);
            response.IdUser = user.Id;
            if (result.Succeeded)
            {
                //Asignando el rol dependiendo el tipo del cliente.
                if (request.SelectRole == ((int)Roles.Admin))
                {
                    user.IsActive = true;
                    await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());

                }
                else
                {

                    await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
                }
            }
            else
            {
                response.HasError = true;
                response.Error = $"Error al registrar al usuario";
                return response;
            }

            return response;
        }

        
        #endregion

        #region Autheincate User
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas registradas con '{request.UserName}'";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Credenciales incorrectas para '{request.UserName}'";
                return response;
            }

            response.Id = user.Id;
            response.UserName = user.UserName;
            response.Email = user.Email;
            response.IdentityCard = user.IdentityCard;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.IsActive = user.IsActive;

            var listRole = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = listRole.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }

        #endregion

        #region Confirm User
        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No hay cuentas registradas con este usuario";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                user.IsActive = true;
                return $"Cuenta confirmada con el correo '{user.Email}' - Ahora puedes disfrutar de la App El Banquillo";

            }
            else
            {
                return $"ha ocurrido un error confirmando '{user.Email}'";
            }














        }
        #endregion

        #region Password

        #region Forgot Password
        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var account = await _userManager.FindByEmailAsync(request.Email);

            if (account == null)
            {
                response.HasError = true;
                response.Error = $"NO EXISTE UNA CUENTA CON ESTE CORREO:{request.Email}";
                return response;
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(account);
            var password = GeneratePassword.Generate();
            var responseChange = await _userManager.ResetPasswordAsync(account, token, password);
            if (!responseChange.Succeeded)
            {
                response.HasError = true;
                response.Error = "OCURRIO UN ERROR RESTABLECIENDO LA CONTRASEÑA";
                return response;
            }

            await _emailService.SendAsync(new EmailRequest()
            {
                To = account.Email,
                Body = $"Hemos restablecido tu contraseña.Esta es tu nueva contraseña: {password}, por favor habla con un admin para que te la cambie por una personalizada",
                Subject = "New Password"

            });

            return response;
        }


        #endregion

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas registradas con '{request.Email}'";
                return response;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Un error al tratar de crear la contraseña";
                return response;
            }

            return response;
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        #endregion

        #region CommonMethods

        #region Update
        public async Task UpdateStatusAsync(string id, bool status)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            user.IsActive = status;
            await _userManager.UpdateAsync(user);
        }

        public async Task UpdateUserAsync(UpdateUserRequest request, string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            #region User Attributes
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.NormalizedEmail = request.Email;
            user.PhoneNumber = request.Phone;
            user.IdentityCard = request.IdentityCard;
            #endregion
            await _userManager.UpdateAsync(user);
        }

        #endregion

        

        public List<AuthenticationResponse> GetAllUsersAsync()
        {
            var user = _userManager.Users.Select(u => new AuthenticationResponse
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                IdentityCard = u.IdentityCard,
                Phone = u.PhoneNumber,
                Roles = _userManager.GetRolesAsync(u).Result.ToList(),
                IsVerified = u.EmailConfirmed,
                IsActive = u.IsActive,

            }).ToList();

            return user;
        }
       
        public async Task<AuthenticationResponse> GetUserByIdAsync(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            var userResponse = new AuthenticationResponse()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                IdentityCard = user.IdentityCard,
                Roles = _userManager.GetRolesAsync(user).Result.ToList(),
                IsVerified = user.EmailConfirmed,
                IsActive = user.IsActive,

            };

            return userResponse;
        }

        #endregion

    }
}

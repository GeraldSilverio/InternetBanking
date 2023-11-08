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

namespace InternetBanking.Infraestructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        public readonly IdentityContext _identityContext;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IdentityContext identityContext)
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
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(user, Roles.Client.ToString());          
                var verificationUrl = await VerificationEmailUrl(user, origin);
                await _emailService.SendAsync(new EmailRequest
                {
                    To = user.Email,
                    Body = $"¡Por favor, haga clic en este enlace para verficar su cuenta! {verificationUrl}",
                    Subject = "Confirmar registro"
                });
            }
            else
            {
                response.HasError = true;
                response.Error = $"Error al registrar al usuario";
                return response;
            }

            return response;
        }

        private async Task<string> VerificationEmailUrl(ApplicationUser user, string origin)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            //User = se refiere al controlador
            var path = "Login/ConfirmEmail";
            var url = new Uri(string.Concat($"{origin}/", path));

            var verificationUrl = QueryHelpers.AddQueryString(url.ToString(), "userId", user.Id);
            verificationUrl = QueryHelpers.AddQueryString(verificationUrl, "token", token);

            return verificationUrl;
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
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"El nombre de usuario '{request.UserName}' no se encuentra registrado";
                return response;
            }

            response.Id = user.Id;
            response.UserName = user.UserName;
            response.Email = user.Email;
            response.IdentityCard = user.IdentityCard;
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;

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

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay cuentas registradas con el correo '{request.Email}'";
                return response;
            }

            var verificationUrl = await ForgotPasswordUrl(user, origin);

            await _emailService.SendAsync(new EmailRequest()
            {
                To = user.Email,
                Body = $"¡Por favor, resete su cuenta visitando esta URL! {verificationUrl}",
                Subject = "Reset password"
            });


            return response;
        }

        private async Task<string> ForgotPasswordUrl(ApplicationUser user, string origin)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var route = "User/ResetPassword";
            var url = new Uri(string.Concat($"{origin}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(url.ToString(), "token", token);

            return verificationUrl;
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

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while reset password";
                return response;
            }

            return response;
        }

        #endregion

        #region Sign Out

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        #endregion

        #region Users 
            public async Task<List<AuthenticationResponse>>  GetAllUsersAsync()
            {
                var user = _userManager.Users.Select(u => new AuthenticationResponse {
                    Id = u.Id,
                    FirstName = u.FirstName, 
                    LastName = u.LastName, 
                    Email = u.Email,
                    IdentityCard = u.IdentityCard,
                    Roles = _userManager.GetRolesAsync(u).Result.ToList(),
                    IsVerified = u.EmailConfirmed                
                }).ToListAsync();

                return await user;               
            }
        #endregion
    }
}

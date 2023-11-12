﻿using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.User
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar su nombre")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe ingresar su nombre")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Debe ingresar su apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe ingresar su correo electrónico")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar un teléfono")]
        [DataType(DataType.Text)]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Debe ingresar una cedula")]
        //[RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "El formato de la  debe ser 809-400-0050")]
        [DataType(DataType.Text)]
        public string IdentityCard { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;
    }
}
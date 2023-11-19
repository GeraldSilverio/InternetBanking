using System.ComponentModel.DataAnnotations;

namespace InternetBanking.Core.Application.ViewModels.User
{
    public class EditUserViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Debe ingresar su nombre")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Debe ingresar su nombre de usuario")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; } = null!;

        [Required(ErrorMessage = "Debe ingresar su apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Debe ingresar su correo electrónico")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Debe ingresar un teléfono")]
        [DataType(DataType.Text)]
        public string? Phone { get; set; }
        public List<string> Roles { get; set; }

        [Required(ErrorMessage = "Debe ingresar una cedula")]     
        [DataType(DataType.Text)]
        public string IdentityCard { get; set; } = null!;
        public decimal Balance { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public DateTime CurrentDate { get; set; } = DateTime.Now;
    }
}

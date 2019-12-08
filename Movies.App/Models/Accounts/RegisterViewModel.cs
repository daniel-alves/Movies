using System.ComponentModel.DataAnnotations;

namespace Movies.App.Models.Accounts
{
    public class RegisterViewModel
    {
        [EmailAddress, Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Senha"),DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Display(Name = "Confirmação da senha")]
        public string ConfirmPassword { get; set; }
    }
}

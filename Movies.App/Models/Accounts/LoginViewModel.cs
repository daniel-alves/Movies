using System.ComponentModel.DataAnnotations;

namespace Movies.App.Models.Accounts
{
    public class LoginViewModel
    {
        [Required, EmailAddress, Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar")]
        public bool RememberMe { get; set; }
    }
}

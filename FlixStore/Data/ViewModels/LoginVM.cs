using System.ComponentModel.DataAnnotations;

namespace FlixStore.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email adress is required")]
        public string EmailAdress { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password {  get; set; }
    }
    public class ForgotPasswordVM
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string EmailAdress { get; set; }
    }
    public class ResetPasswordVM
    {
        [Required]
        [EmailAddress]
        public string EmailAdress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FlixStore.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email adress is required")]
        public string EmailAdress { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password {  get; set; }

        [Display(Name ="Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}

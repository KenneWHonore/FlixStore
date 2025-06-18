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
}

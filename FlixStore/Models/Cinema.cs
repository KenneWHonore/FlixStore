using System.ComponentModel.DataAnnotations;
using FlixStore.Data.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FlixStore.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Cinema logo is required")]
        public string Logo { get; set; }

        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Cinema name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Cinema description is required")]
        public string Description { get; set; }

        //Relationships
        [ValidateNever]
        public List<Movie> Movies { get; set; }
    }
}

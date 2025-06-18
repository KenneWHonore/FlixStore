using FlixStore.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using FlixStore.Data.Base;

public class Actor : IEntityBase
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Profile Picture")]
    [Required(ErrorMessage = "Profile Picture is required")]
    public string ProfilePictureURL { get; set; }

    [Display(Name = "Full Name")]
    [Required(ErrorMessage = "Full Name is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Full name must be between 3 and 50 chars")]
    public string FullName { get; set; }

    [Display(Name = "Biography")]
    [Required(ErrorMessage = "Biography is required")]
    public string Bio { get; set; }

    [ValidateNever]
    public List<Actor_Movie> Actors_Movies { get; set; } = new List<Actor_Movie>();
}

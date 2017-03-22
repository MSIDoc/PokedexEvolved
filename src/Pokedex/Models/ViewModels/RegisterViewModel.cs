using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Models.ViewModels
{
    public class RegisterViewModel
    {
        public RegisterViewModel()
        {

        }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        [StringLength(maximumLength: 25, ErrorMessage = "First name must be less than 25 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name must be less than 50 characters.")]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "You must provide a password")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must enter your confirm password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Pokedex.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(length:5)]
        [MaxLength(length:20, ErrorMessage = "Username cannot be more than 20 characters")]        
        public string Username { get; set; }

        [Required]
        [MinLength(length:6)]           
        [DataType(dataType: DataType.Password)]     
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
namespace WebAPI.ViewModels
{
    public class LoginViewModel
    {
        
        [Required]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]        
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
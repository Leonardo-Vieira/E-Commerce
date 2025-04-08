using System.ComponentModel.DataAnnotations;

namespace Authentication.Models
{
    public class Login
    {
        [Required]
        public string Email { get; set; } 
        [Required]
        public string Password { get; set; } 
    }
}
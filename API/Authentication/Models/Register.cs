using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Models
{
    public class Register
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "password must contain 8 characters")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string PostalCode { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsCollaborator { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string IdentificationNumber { get; set; }

        public string PostalCode { get; set; }

        public string Place { get; set; }

        public string Address { get; set; }
        public bool IsCollaborator { get; set; }
    }
}
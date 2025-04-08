using System;
using System.ComponentModel.DataAnnotations;
using Domain_Core.Models;

namespace E_Client.Models
{
    public class Person : Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string IdentificationNumber { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
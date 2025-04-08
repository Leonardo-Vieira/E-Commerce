using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain_Core.Models;
using e_order.Domain.Models;

namespace e_order.Domain.ViewModels
{
    public class ClientViewModel
    {
        public PersonViewModel Person { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Hour { get; set; }
    }
}
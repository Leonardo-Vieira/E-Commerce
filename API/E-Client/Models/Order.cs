using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain_Core.Models;

namespace E_Client.Models
{
    public class Order : Entity
    {
        public Order(Guid id, Guid? clientId, bool state)
        {
            Id = id;
            ClientId = clientId;
            DateOrder = DateTime.Now;
            State = state;
        }
        public Order()
        {
        }

        [ForeignKey("ClientId")]
        public Guid? ClientId { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public DateTime DateOrder { get; set; }

        [Required]
        public bool State { get; set; }
    }
}
using Domain_Core.Commands;
using System;

namespace E_Product.Domain.Commands.Provider
{
    public abstract class ProviderCommand : Command
    {
        public Guid Id {get;set;}
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public string Place { get; set; }
        public string IdentificationNumber { get; set; }
    }
}
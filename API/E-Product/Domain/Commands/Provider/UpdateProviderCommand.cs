using System;

namespace E_Product.Domain.Commands.Provider
{
    public class UpdateProviderCommand : ProviderCommand
    {
        public UpdateProviderCommand(Guid id, string code, string name, string description, string phone, string postalCode, string place, string identificationNumber)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            Phone = phone;
            PostalCode = postalCode;
            Place = place;
            IdentificationNumber = identificationNumber;
        }
        public override bool IsValid () 
        {
            //   ValidationResult = new CreateBrandCommandValidation();
            //return ValidationResult.IsValid;
            return true;
        }
    }
}
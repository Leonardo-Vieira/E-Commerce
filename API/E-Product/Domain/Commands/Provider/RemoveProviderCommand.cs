using System;

namespace E_Product.Domain.Commands.Provider
{
    public class RemoveProviderCommand : ProviderCommand
    {
        public RemoveProviderCommand(Guid id)
        {
            Id = id;
        }
        public override bool IsValid () 
        {
            //   ValidationResult = new CreateBrandCommandValidation();
            //return ValidationResult.IsValid;
            return true;
        }
    }
}
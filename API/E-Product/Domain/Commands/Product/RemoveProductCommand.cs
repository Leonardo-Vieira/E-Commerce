using System;

namespace E_Product.Domain.Commands.Product
{
    public class RemoveProductCommand : ProductCommand
    {
        public RemoveProductCommand(Guid id)
        {
           Id = id;
        }
        public override bool IsValid()
        {
            //ValidationResult = new RemoveBrandCommandValidation();
            return ValidationResult.IsValid;
        }
    }
}
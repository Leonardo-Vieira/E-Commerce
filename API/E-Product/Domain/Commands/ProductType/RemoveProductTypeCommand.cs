using System;

namespace E_Product.Domain.Commands.ProductType
{
    public class RemoveProductTypeCommand : ProductTypeCommand
    {
        public RemoveProductTypeCommand(Guid id)
        {
            Id = id;
        }
         public override bool IsValid()
        {
         //   ValidationResult = new CreateBrandCommandValidation();
            return ValidationResult.IsValid;
        }
    }
}
using System;
using E_Product.Domain.Validations;

namespace E_Product.Domain.Commands.Brand
{
    public class RemoveBrandCommand : BrandCommand
    {
        public RemoveBrandCommand(Guid id)
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
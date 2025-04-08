using System;

namespace E_Product.Domain.Commands.ProductType
{
    public class UpdateProductTypeCommand : ProductTypeCommand
    {
        public UpdateProductTypeCommand(Guid id, string code, string name, string description)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
        }
        public override bool IsValid()
        {
         //   ValidationResult = new CreateBrandCommandValidation();
            return ValidationResult.IsValid;
        }
    }
}
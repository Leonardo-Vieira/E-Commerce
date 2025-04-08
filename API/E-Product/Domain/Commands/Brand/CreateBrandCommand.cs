using System;
using E_Product.Domain.Validations;

namespace E_Product.Domain.Commands.Brand
{
    public class CreateBrandCommand : BrandCommand
    {
        public CreateBrandCommand(string code, string name, string description)
        {
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
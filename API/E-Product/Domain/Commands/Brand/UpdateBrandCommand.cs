using System;
using E_Product.Domain.Validations;

namespace E_Product.Domain.Commands.Brand
{
    public class UpdateBrandCommand : BrandCommand
    {
         public UpdateBrandCommand(Guid id, string code, string name, string description)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
        }

        public override bool IsValid()
        {
        //  ValidationResult = new UpdateBrandCommandValidation();
            return ValidationResult.IsValid;
        }
    }
}
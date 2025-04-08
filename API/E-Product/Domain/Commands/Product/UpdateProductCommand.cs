using System;
using E_Product.Models;

namespace E_Product.Domain.Commands.Product
{
    public class UpdateProductCommand : ProductCommand
    {
          public UpdateProductCommand(Guid id, string code, string name, string description, bool status, decimal price, int stock, Guid? providerId, Guid? brandId, Guid? productTypeId)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
            Status = status;
            Price = price;
            Stock = stock;
            ProviderId = providerId;
            BrandId = brandId;
            ProductTypeId = productTypeId;
        }
         public override bool IsValid()
        {
         //   ValidationResult = new CreateBrandCommandValidation();
            return ValidationResult.IsValid;
        }
    }
}
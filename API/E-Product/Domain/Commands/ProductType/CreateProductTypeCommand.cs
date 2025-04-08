namespace E_Product.Domain.Commands.ProductType
{
    public class CreateProductTypeCommand : ProductTypeCommand
    {
        public CreateProductTypeCommand(string code, string name, string description)
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
namespace E_Product.Domain.Commands.Provider
{
    public class CreateProviderCommand : ProviderCommand 
    {
        public CreateProviderCommand (string code, string name, string description, string phone, string postalCode, string place, string identificationNumber) 
        {
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
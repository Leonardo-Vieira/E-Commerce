using System;

namespace E_Product.Models
{
    public class ProductToView
    {
       /*  public ProductToView(Guid productId, string code, string name, bool status, string description, double price, int stock, Provider provider, Brand brand, ProductType productType)
        {
            ProductId = productId;
            Code = code;
            Name = name;
            Status = status;
            Description = description;
            Price = price;
            Stock = stock;
            Provider = provider;
            Brand = brand;
            ProductType = productType;

        } */
      /*   public ProductToView()
        {
            
        } */
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public  Provider Provider {get; set;}
        public  Brand Brand { get; set; }
        public  ProductType ProductType { get; set; }
    }}
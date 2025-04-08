using System;
using Domain_Core.Models;

namespace E_Product.Models
{
    public class Product
    {
      /*   public Product(Guid id, string code, string name, bool status, string description, decimal price, int stock, Guid? providerId, Guid? brandId, Guid? productTypeId)
        {
            Id = id;
            Code = code;
            Name = name;
            Status = status;
            Description = description;
            Price = price;
            Stock = stock;
            ProviderId = providerId;
            BrandId = brandId;
            ProductTypeId = productTypeId;
        }
 */     public Product() { }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
//        public Guid ProviderId { get; set; }
        public  Provider Provider {get; set;}
        //public Guid BrandId { get; set; }
        public  Brand Brand { get; set; }
        //public Guid ProductTypeId { get; set; }
        public  ProductType ProductType { get; set; }
    }
}
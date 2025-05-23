using System;
using Domain_Core.Commands;

namespace E_Product.Domain.Commands.Brand
{
    public abstract class BrandCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Code { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
    }
}
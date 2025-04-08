using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using Domain_Core.CommandHandlers;
using E_Product.Domain.Commands.Product;
using E_Product.Domain.Events.Product;
using E_Product.Models;
using E_Product.Repository;
using MediatR;

namespace E_Product.Domain.CommandHandlers
{
    public class ProductCommandHandler : 
    CommandHandler, 
    IRequestHandler<CreateProductCommand>, 
    IRequestHandler<RemoveProductCommand>, 
    IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _repo;
        private readonly IMediatorHandler _bus;
        public ProductCommandHandler (IProductRepository repo, IMediatorHandler bus) : base (bus) 
        {
            _bus = bus;
            _repo = repo;
        }
        public Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = Mapper.Map<Product>(request);
            
            _repo.Create(newProduct);
            _bus.RaiseEvent(Mapper.Map<ProductCreatedEvent>(newProduct));

            _repo.Save();
            return Task.CompletedTask;
        }

        public Task Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            _repo.Remove(request.Id);
            _bus.RaiseEvent(new ProductRemovedEvent(request.Id));

            _repo.Save();
            return Task.CompletedTask;
        }

        public Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var updatedProduct = Mapper.Map<Product>(request);
           /*   var updatedProduct = new Product(request.Id, request.Code, request.Name, 
            request.Status, request.Description, request.Price, request.Stock, request.ProviderId, request.BrandId, request.ProductTypeId); */

            _repo.Update (updatedProduct);

            _bus.RaiseEvent (Mapper.Map<ProductUpdatedEvent>(updatedProduct));

            _repo.Save();
            return Task.CompletedTask;
        }
    }
}
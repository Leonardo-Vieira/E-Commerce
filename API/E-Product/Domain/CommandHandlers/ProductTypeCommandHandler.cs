using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using Domain_Core.CommandHandlers;
using E_Product.Domain.Commands.ProductType;
using E_Product.Domain.Events.ProductType;
using E_Product.Models;
using E_Product.Repository;
using MediatR;

namespace E_Product.Domain.CommandHandlers
{
    public class ProductTypeCommandHandler : 
    CommandHandler, 
    IRequestHandler<CreateProductTypeCommand>, 
    IRequestHandler<UpdateProductTypeCommand>, 
    IRequestHandler<RemoveProductTypeCommand> 
    {
        private readonly IProductTypeRepository _repo;
        private readonly IMediatorHandler _bus;
        public ProductTypeCommandHandler (IProductTypeRepository repo, IMediatorHandler bus) : base (bus) {
            _bus = bus;
            _repo = repo;

        }
        public Task Handle (CreateProductTypeCommand request, CancellationToken cancellationToken) {
            var newProductType = Mapper.Map<ProductType>(request);
            _repo.Create (newProductType);
            _bus.RaiseEvent (Mapper.Map<ProductTypeCreatedEvent>(newProductType));

            _repo.Save();
            return Task.CompletedTask;
        }

        public Task Handle (UpdateProductTypeCommand request, CancellationToken cancellationToken) {
            var updatedProductType = Mapper.Map<ProductType>(request);
            _repo.Update (updatedProductType);
            _bus.RaiseEvent(Mapper.Map<ProductTypeUpdatedEvent>(updatedProductType));


            _repo.Save();
            return Task.CompletedTask;
        }

        public Task Handle (RemoveProductTypeCommand request, CancellationToken cancellationToken) {
            _repo.Remove (request.Id);
            _bus.RaiseEvent (new ProductTypeRemovedEvent (request.Id));

            _repo.Save();
            return Task.CompletedTask;
        }
    }
}
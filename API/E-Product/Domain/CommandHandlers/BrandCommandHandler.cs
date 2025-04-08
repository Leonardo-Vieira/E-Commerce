using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using Domain_Core.CommandHandlers;
using Domain_Core.Events;
using E_Product.Domain.Commands.Brand;
using E_Product.Domain.Events.Brand;
using E_Product.Models;
using E_Product.Repository;
using MediatR;

namespace E_Product.Domain.CommandHandlers {
    public class BrandCommandHandler : 
     CommandHandler,
     IRequestHandler<CreateBrandCommand>, 
     IRequestHandler<RemoveBrandCommand>, 
     IRequestHandler<UpdateBrandCommand> 
     {
        private readonly IBrandRepository _repo;
        private readonly IMediatorHandler _bus;
        public BrandCommandHandler (IBrandRepository repo, IMediatorHandler bus) : base(bus) {
            _bus = bus;
            _repo = repo;
        }
        public Task Handle (CreateBrandCommand request, CancellationToken cancellationToken) {
            // if (!request.IsValid ())
            //     return Task.CompletedTask;

            var newBrand = Mapper.Map<Brand>(request);
            _repo.Create(newBrand);
            _bus.RaiseEvent(Mapper.Map<BrandCreatedEvent>(newBrand));

            _repo.Save();
            return Task.CompletedTask;
        }
        public Task Handle (UpdateBrandCommand request, CancellationToken cancellationToken) {
            // if (!request.IsValid ())
            // return Task.CompletedTask;

            var updatedBrand = Mapper.Map<Brand>(request);
            _repo.Update(updatedBrand);
            _bus.RaiseEvent(Mapper.Map<BrandUpdatedEvent>(updatedBrand));

            _repo.Save();
            return Task.CompletedTask;
        }

        public Task Handle (RemoveBrandCommand request, CancellationToken cancellationToken) {
            // if (!request.IsValid ())
            //     return Task.CompletedTask;
            _repo.Remove (request.Id);
            _bus.RaiseEvent (new BrandRemovedEvent (request.Id));


            _repo.Save();
            return Task.CompletedTask;
        }
    }
}
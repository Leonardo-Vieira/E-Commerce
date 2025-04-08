using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using Domain_Core.CommandHandlers;
using E_Product.Domain.Commands.Provider;
using E_Product.Domain.Events.Provider;
using E_Product.Models;
using E_Product.Repository;
using MediatR;

namespace E_Product.Domain.CommandHandlers {
    public class ProviderCommandHandler : 
    CommandHandler, 
    IRequestHandler<CreateProviderCommand>, 
    IRequestHandler<UpdateProviderCommand>, 
    IRequestHandler<RemoveProviderCommand> 
    {
        private readonly IProviderRepository _repo;
        private readonly IMediatorHandler _bus;
        public ProviderCommandHandler (IProviderRepository repo, IMediatorHandler bus) : base (bus) {
            _bus = bus;
            _repo = repo;

        }
        public Task Handle (CreateProviderCommand request, CancellationToken cancellationToken) {
            if (!request.IsValid ())
                return Task.CompletedTask;

            var newProvider = Mapper.Map<Provider>(request);
            _repo.Create(newProvider);

            _bus.RaiseEvent (Mapper.Map<ProviderCreatedEvent>(newProvider));


            _repo.Save();
            return Task.CompletedTask;
        }

        public Task Handle (UpdateProviderCommand request, CancellationToken cancellationToken) {
             if (!request.IsValid ())
                return Task.CompletedTask;

           // var updatedProvider = new Provider(request.ProviderId, request.Code, request.Name,
           // request.Description, request.Phone, request.PostalCode, request.Place, request.IdentificationNumber);
            var updatedProvider = Mapper.Map<Provider>(request);  

            _repo.Update(updatedProvider);

           // _bus.RaiseEvent(new ProviderUpdatedEvent(updatedProvider.Id, updatedProver.Code,
           // updatedProvider.Name, updatedProvider.Description, updatedProvider.Phone, updatedProvider.PostalCode, updatedProvider.Place, updatedProvider.IdentificationNumber));
            _bus.RaiseEvent(Mapper.Map<ProviderUpdatedEvent>(updatedProvider));


            _repo.Save();
            return Task.CompletedTask;
        }

        public Task Handle (RemoveProviderCommand request, CancellationToken cancellationToken) {
             if (!request.IsValid ())
                return Task.CompletedTask;
            _repo.Remove(request.Id);
            _bus.RaiseEvent(new ProviderRemovedEvent(request.Id));


            _repo.Save();
            return Task.CompletedTask;
        }
    }
}
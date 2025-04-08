using System.Threading.Tasks;
using e_order.Repository;
using e_order.Domain.IntegrationEvents.Client;
using e_order.Domain.Models;
using Domain_Core.Events;
using Newtonsoft.Json;
using Domain_Core.Bus;
using System;
using e_order.Data.Repository;
using e_order.Domain.IntegrationEvents.Brand;

namespace e_order.Domain.IntegrationEventHandlers
{
    public class BrandIntegrationEventHandler : IEventHandler<BrandCreatedIntegrationEvent>, IEventHandler<BrandRemovedIntegrationEvent>, IEventHandler<BrandUpdatedIntegrationEvent>
    {
        private readonly IBrandRepository _repo;
        private readonly IEventStoreRepository _eventStore;

        public BrandIntegrationEventHandler(IBrandRepository repo, IEventStoreRepository eventStore)
        {
            _eventStore = eventStore;
            _repo = repo;
        }
        public Task Handle(BrandCreatedIntegrationEvent @event)
        {
            var newBrand = new Brand(@event.Id, @event.Code, @event.Name, @event.Description);
            _repo.Create(newBrand);
            _repo.Save();

            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            return Task.CompletedTask;
        }
        public Task Handle(BrandRemovedIntegrationEvent @event)
        {
            _repo.Remove(@event.Id);
            _repo.Save();
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            return Task.CompletedTask;
        }
        public Task Handle(BrandUpdatedIntegrationEvent @event)
        {
            var updateBrand = new Brand(@event.Id, @event.Code, @event.Name, @event.Description);
            _repo.Update(updateBrand);
            _repo.Save();

            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            return Task.CompletedTask;
        }
    }
}
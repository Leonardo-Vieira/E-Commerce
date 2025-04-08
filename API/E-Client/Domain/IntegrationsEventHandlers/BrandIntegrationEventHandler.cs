using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.Events;
using E_Client.Domain.IntegrationEvents.Brand;
using E_Client.Domain.Interface;
using E_Client.Models;
using Newtonsoft.Json;

namespace E_Client.Domain.IntegrationsEventHandlers
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
            Brand newBrand = new Brand(@event.Id, @event.Code, @event.Name, @event.Description);
            _repo.Create(newBrand);

            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            return Task.CompletedTask;
        }

        public Task Handle(BrandRemovedIntegrationEvent @event)
        {
            _repo.Remove(@event.Id);
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            return Task.CompletedTask;
        }
        public Task Handle(BrandUpdatedIntegrationEvent @event)
        {
            Brand updatedBrand = new Brand(@event.Id, @event.Code, @event.Name, @event.Description);

            _repo.Update(updatedBrand);

            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            return Task.CompletedTask;
        }
    }
}
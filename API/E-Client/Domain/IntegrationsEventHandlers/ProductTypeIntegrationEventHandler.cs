using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.Events;
using E_Client.Domain.IntegrationEvents.ProductType;
using E_Client.Domain.Interface;
using E_Client.Models;
using Newtonsoft.Json;

namespace E_Client.Domain.IntegrationsEventHandlers
{
    public class ProductTypeIntegrationEventHandler : IEventHandler<ProductTypeCreatedIntegrationEvent>, IEventHandler<ProductTypeRemovedIntegrationEvent>, IEventHandler<ProductTypeUpdatedIntegrationEvent>
    {
        private readonly IProductTypeRepository _repo;
        private readonly IEventStoreRepository _eventStore;
        public ProductTypeIntegrationEventHandler(IProductTypeRepository repo, IEventStoreRepository eventStore)
        {
            _eventStore = eventStore;
            _repo = repo;
        }
        
        public Task Handle(ProductTypeCreatedIntegrationEvent @event)
        {
            ProductType newProductType = new ProductType(@event.Id, @event.Code, @event.Name, @event.Description);
            _repo.Create(newProductType);

            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            return Task.CompletedTask;
        }

        public Task Handle(ProductTypeRemovedIntegrationEvent @event)
        {
            _repo.Remove(@event.Id);
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            return Task.CompletedTask;
        }

        public Task Handle(ProductTypeUpdatedIntegrationEvent @event)
        {
            ProductType updatedProductType = new ProductType(@event.Id, @event.Code, @event.Name, @event.Description);
            _repo.Update(updatedProductType);
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            return Task.CompletedTask;
        }
    }
}
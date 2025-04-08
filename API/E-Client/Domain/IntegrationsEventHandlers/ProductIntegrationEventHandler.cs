using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.Events;
using E_Client.Domain.IntegrationEvents.Product;
using E_Client.Models;
using E_Client.Repository;
using Newtonsoft.Json;

namespace E_Client.Domain.IntegrationsEventHandlers
{
    public class ProductIntegrationEventHandler : IEventHandler<ProductCreatedIntegrationEvent>, IEventHandler<ProductRemovedIntegrationEvent>, IEventHandler<ProductUpdatedIntegrationEvent>
    {
        private readonly IProductRepository _repo;
        private readonly IEventStoreRepository _eventStore;
        public ProductIntegrationEventHandler(IProductRepository repo, IEventStoreRepository eventStore)
        {
            _repo = repo;
            _eventStore = eventStore;
        }

        public Task Handle(ProductCreatedIntegrationEvent @event)
        {
            Product newProduct = new Product(@event.Id, @event.BrandId, @event.ProductTypeId,
              @event.Name, @event.Description, @event.Status, @event.Price);
            _repo.Create(newProduct);

            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            return Task.CompletedTask;
        }

        public Task Handle(ProductRemovedIntegrationEvent @event)
        {
            _repo.Remove(@event.Id);
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            return Task.CompletedTask;
        }

        public Task Handle(ProductUpdatedIntegrationEvent @event)
        {
            Product updatedProduct = new Product(@event.Id, @event.BrandId, @event.ProductTypeId,
            @event.Name, @event.Description, @event.Status, @event.Price);

            _repo.Update(updatedProduct);

            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            return Task.CompletedTask;
        }
    }
}
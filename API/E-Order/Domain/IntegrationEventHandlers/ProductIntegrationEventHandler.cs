using System;
using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.Events;
using e_order.Domain.IntegrationEvents.Product;
using e_order.Domain.Models;
using e_order.Repository;
using Newtonsoft.Json;

namespace e_order.Domain.IntegrationsEventHandlers
{
    public class ProductIntegrationEventHandler : IEventHandler<ProductCreatedIntegrationEvent>, IEventHandler<ProductRemovedIntegrationEvent>, IEventHandler<ProductUpdatedIntegrationEvent>
    {
        private readonly IProductRepository _repo;
        private readonly IEventStoreRepository _eventStore;

        public ProductIntegrationEventHandler(IProductRepository repo, IEventStoreRepository eventStore)
        {
            _eventStore = eventStore;
            _repo = repo;
        }

        public Task Handle(ProductCreatedIntegrationEvent @event)
        {
            var newProduct = new Product(@event.Id, @event.Code, @event.Name, @event.Status, @event.Description, @event.Price, @event.Stock, @event.BrandId);
            _repo.Create(newProduct);
            _repo.Save();
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            return Task.CompletedTask;
        }

        public Task Handle(ProductRemovedIntegrationEvent @event)
        {
            _repo.Remove(@event.Id);
            _repo.Save();
             _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            
            return Task.CompletedTask;
        }

        public Task Handle(ProductUpdatedIntegrationEvent @event)
        {
            var updatedProduct = new Product(@event.Id, @event.Code, @event.Name, @event.Status, @event.Description, @event.Price, @event.Stock, @event.BrandId);
            _repo.Update(updatedProduct);
            _repo.Save();
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            return Task.CompletedTask;
        }
    }
}
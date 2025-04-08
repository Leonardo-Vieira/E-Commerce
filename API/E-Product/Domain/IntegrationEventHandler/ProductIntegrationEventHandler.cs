using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.Events;
using E_Product.Domain.IntegrationEvents;
using E_Product.Models;
using E_Product.Repository;
using Newtonsoft.Json;

namespace E_Product.Domain.IntegrationEventHandler {
    public class ProductIntegrationEventHandler : 
    IEventHandler<ProductOrderRequestedIntegrationEvent>, 
    IEventHandler<ProductOrderCancelledIntegrationEvent> 
    {
        private readonly IProductRepository _repo;
        private readonly IEventStoreRepository _eventStore;
        public ProductIntegrationEventHandler (IProductRepository repo, IEventStoreRepository eventStore) {
            _eventStore = eventStore;
            _repo = repo;
        }
        public async Task Handle (ProductOrderRequestedIntegrationEvent @event) 
        {
            var productToUpdate = await _repo.GetById(@event.ProductId);
            
            productToUpdate.Stock = productToUpdate.Stock - @event.Quantity;
            _repo.Update(productToUpdate);
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            await Task.CompletedTask;
        }


        public async Task Handle(ProductOrderCancelledIntegrationEvent @event)
        {
            var productToUpdate = await _repo.GetById(@event.ProductId);
            if(productToUpdate != null)
            {
                productToUpdate.Stock = productToUpdate.Stock + @event.Quantity;
                _repo.Update(productToUpdate);
                _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            }

            await Task.CompletedTask;    
        
        }
    }
}
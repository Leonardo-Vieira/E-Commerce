using System;
using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.Events;
using E_Client.Domain.Events.OrderItem;
using E_Client.Models;
using E_Client.Repository;
using Newtonsoft.Json;

namespace E_Client.Domain.IntegrationsEventHandlers
{
    public class OrderItemIntegrationEventHandler : IEventHandler<OrderItemCreatedIntegrationEvent>, IEventHandler<OrderItemRemovedIntegrationEvent>, IEventHandler<OrderItemUpdatedIntegrationEvent>
    {
        private readonly IOrderItemRepository _repo;
        private readonly IEventStoreRepository _eventStore;
        public OrderItemIntegrationEventHandler(IOrderItemRepository repo, IEventStoreRepository eventStore)
        {
            _repo = repo;
            _eventStore = eventStore;
        }

        public Task Handle(OrderItemCreatedIntegrationEvent @event)
        {
           /*  OrderItem newOrderItem = new OrderItem(Guid.NewGuid(), @event.ProductId, @event.OrderId, @event.Quantity);
            _repo.Create(newOrderItem);

            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event))); */

            return Task.CompletedTask;
        }

        public Task Handle(OrderItemRemovedIntegrationEvent @event)
        {
            _repo.Remove(@event.Id);
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemUpdatedIntegrationEvent @event)
        {
            /* OrderItem updatedOrderItem = new OrderItem(Guid.NewGuid(), @event.ProductId, @event.OrderId, @event.Quantity);
            _repo.Update(updatedOrderItem);

            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event))); */
            return Task.CompletedTask;
        }
    }
}
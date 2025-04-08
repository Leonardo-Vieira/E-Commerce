using System;
using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.Events;
using E_Client.Domain.Events.Order;
using E_Client.Models;
using E_Client.Repository;
using Newtonsoft.Json;

namespace E_Client.Domain.IntegrationsEventHandlers
{
    public class OrderIntegrationEventHandler : IEventHandler<OrderCreatedIntegrationEvent>, IEventHandler<OrderRemovedIntegrationEvent>, IEventHandler<OrderUpdatedIntegrationEvent>
    {
        private readonly IOrderRepository _repo;
        private readonly IEventStoreRepository _eventStore;
        public OrderIntegrationEventHandler(IOrderRepository repo, IEventStoreRepository eventStore)
        {
            _repo = repo;
            _eventStore = eventStore;
        }

        public Task Handle(OrderCreatedIntegrationEvent @event)
        {
            Order newOrder = new Order(Guid.NewGuid(), @event.ClientId, @event.State);
            _repo.Create(newOrder);

            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            return Task.CompletedTask;
        }

        public Task Handle(OrderRemovedIntegrationEvent @event)
        {
            _repo.Remove(@event.Id);
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            return Task.CompletedTask;
        }

        public Task Handle(OrderUpdatedIntegrationEvent @event)
        {
            Order updatedOrder = new Order(@event.Id, @event.ClientId, @event.State);

            _repo.Update(updatedOrder);

            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            return Task.CompletedTask;
        }
    }
}
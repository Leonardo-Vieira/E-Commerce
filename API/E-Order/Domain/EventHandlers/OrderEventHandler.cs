using System.Threading;
using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.Events;
using e_order.Domain.Event.Order;
using MediatR;
using e_order.Domain.IntegrationEvents.Client;
using System;

namespace e_order.Domain.EventHandlers
{
    public class OrderEventHandler :
    INotificationHandler<OrderCreatedEvent>,
    INotificationHandler<OrderUpdatedEvent>,
    INotificationHandler<OrderRemovedEvent>
    {
        private readonly IEventBus _bus;
        public OrderEventHandler(IEventBus bus)
        {
            _bus = bus;

        }
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            //_bus.Publish(new OrderCreatedIntegrationEvent(notification.Id, notification.ClientId, notification.Date, notification.State));
            return Task.CompletedTask;
        }

        public Task Handle(OrderRemovedEvent notification, CancellationToken cancellationToken)
        {
            //_bus.Publish(new OrderRemovedIntegrationEvent(notification.Id));
            return Task.CompletedTask;
        }

        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            //_bus.Publish(new OrderUpdatedIntegrationEvent(notification.Id, notification.ClientId, notification.DateOrder, notification.State));
            return Task.CompletedTask;
        }
    }
}
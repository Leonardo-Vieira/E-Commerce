using System.Threading;
using System.Threading.Tasks;
using e_order.Domain.Event.OrderItem;
using Domain_Core.Bus;
using MediatR;
using e_order.Domain.Event.Order;
using System;

namespace e_order.Domain.EventHandlers
{
    public class OrderItemEventHandler :
    INotificationHandler<ProductOrderRequestedEvent>
    {
        private readonly IEventBus _bus;
        public OrderItemEventHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public Task Handle(ProductOrderRequestedEvent notification, CancellationToken cancellationToken)
        {
            //_bus.Publish(new ProductOrderRequestedEvent(notification.ProductId, notification.Quantity));
            return Task.CompletedTask;
        }
    }
}
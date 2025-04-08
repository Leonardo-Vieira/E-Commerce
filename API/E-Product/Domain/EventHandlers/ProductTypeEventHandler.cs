using System.Threading;
using System.Threading.Tasks;
using Apache.NMS.Util;
using Domain_Core.Bus;
using Domain_Core.Events;
using E_Product.Domain.Events.ProductType;
using E_Product.Domain.IntegrationEvents;
using MediatR;
using Newtonsoft.Json;

namespace E_Product.Domain.EventHandlers
{
    public class ProductTypeEventHandler : INotificationHandler<ProductTypeCreatedEvent>,  INotificationHandler<ProductTypeRemovedEvent>,  INotificationHandler<ProductTypeUpdatedEvent>
    {
        private readonly IEventBus _bus;
        public ProductTypeEventHandler(IEventBus bus) 
        {
            _bus = bus;
        }

        public Task Handle(ProductTypeCreatedEvent notif, CancellationToken cancellationToken)
        {
            _bus.Publish(new ProductTypeCreatedIntegrationEvent(notif.Id, notif.Code, notif.Name, notif.Description));
            return Task.CompletedTask;
        }

        public Task Handle(ProductTypeRemovedEvent notif, CancellationToken cancellationToken)
        {
            _bus.Publish(new ProductTypeRemovedIntegrationEvent(notif.Id));
            return Task.CompletedTask;
        }

        public Task Handle(ProductTypeUpdatedEvent notif, CancellationToken cancellationToken)
        {
            _bus.Publish(new ProductTypeUpdatedIntegrationEvent(notif.Id, notif.Code, notif.Name, notif.Description));
            return Task.CompletedTask;
        }
    }
}
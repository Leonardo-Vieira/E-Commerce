using System.Threading;
using System.Threading.Tasks;
using Apache.NMS.Util;
using Domain_Core.Bus;
using Domain_Core.Events;
using E_Product.Domain.Events.Product;
using E_Product.Domain.IntegrationEvents;
using MediatR;
using Newtonsoft.Json;

namespace E_Product.Domain.EventHandlers
{
    public class ProductEventHandler : INotificationHandler<ProductCreatedEvent>, INotificationHandler<ProductRemovedEvent>, INotificationHandler<ProductUpdatedEvent>
    {
        private readonly IEventBus _bus;
        public ProductEventHandler (IEventBus bus) 
        {
            _bus = bus;
        }

        public Task Handle(ProductCreatedEvent notif, CancellationToken cancellationToken)
        {
            _bus.Publish(new ProductCreatedIntegrationEvent(notif.Id, notif.Code, notif.Name, notif.Status, 
            notif.Description, notif.Price, notif.Stock, notif.BrandId, notif.ProductTypeId));
            return Task.CompletedTask;
        }

        public Task Handle(ProductRemovedEvent notif, CancellationToken cancellationToken)
        {
            _bus.Publish(new ProductRemovedIntegrationEvent(notif.Id));
             return Task.CompletedTask;
        }

        public Task Handle(ProductUpdatedEvent notif, CancellationToken cancellationToken)
        {
            _bus.Publish(new ProductUpdatedIntegrationEvent(notif.Id, notif.Code, notif.Name, notif.Status,
             notif.Description, notif.Price, notif.Stock, notif.BrandId, notif.ProductTypeId));
             return Task.CompletedTask;
        }
    }
}
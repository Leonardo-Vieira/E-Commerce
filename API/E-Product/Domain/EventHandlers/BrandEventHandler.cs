using System.Threading;
using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.Events;
using E_Product.Domain.Events.Brand;
using E_Product.Domain.IntegrationEvents;
using MediatR;

namespace E_Product.Domain.EventHandlers {
    public class BrandEventHandler : INotificationHandler<BrandCreatedEvent>, INotificationHandler<BrandRemovedEvent>, INotificationHandler<BrandUpdatedEvent>{
        private readonly IEventBus _bus;

        public BrandEventHandler (IEventBus bus) {
            _bus = bus;
        }

        public Task Handle(BrandCreatedEvent notif, CancellationToken cancellationToken)
        {
            _bus.Publish(new BrandCreatedIntegrationEvent(notif.Id, notif.Code, notif.Name, notif.Description));
            return Task.CompletedTask;
        }

        public Task Handle(BrandRemovedEvent notif, CancellationToken cancellationToken)
        {
           _bus.Publish(new BrandRemovedIntegrationEvent(notif.Id));
            return Task.CompletedTask;
        }

        public Task Handle(BrandUpdatedEvent notif, CancellationToken cancellationToken)
        {
            _bus.Publish(new BrandUpdatedIntegrationEvent(notif.Id, notif.Code, notif.Name, notif.Description));
            return Task.CompletedTask;
        }

    }
}
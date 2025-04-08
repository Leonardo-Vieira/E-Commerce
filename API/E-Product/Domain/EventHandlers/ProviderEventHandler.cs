using System.Threading;
using System.Threading.Tasks;
using Apache.NMS.Util;
using Domain_Core.Bus;
using Domain_Core.Events;
using E_Product.Domain.Events.Provider;
using MediatR;
using Newtonsoft.Json;

namespace E_Product.Domain.EventHandlers
{
    public class ProviderEventHandler : INotificationHandler<ProviderCreatedEvent>,  INotificationHandler<ProviderRemovedEvent>,  INotificationHandler<ProviderUpdatedEvent>
    {
        public ProviderEventHandler()
        {
        }

        public Task Handle(ProviderCreatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProviderRemovedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ProviderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
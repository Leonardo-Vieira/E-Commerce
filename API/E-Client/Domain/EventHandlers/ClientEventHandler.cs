using System.Threading;
using System.Threading.Tasks;
using Domain_Core.Bus;
using E_Client.Domain.Events.Client;
using MediatR;
using E_Client.Domain.IntegrationEvents.Client;
using E_Client.Domain.IntegrationEvents.Person;

namespace E_Client.Domain.EventHandlers
{
    public class ClientEventHandler : INotificationHandler<ClientRegisteredEvent>, INotificationHandler<ClientLoginEvent>, INotificationHandler<ClientRemovedEvent>, INotificationHandler<ClientUpdatedEvent>
    {
        private readonly IEventBus _bus;

        public ClientEventHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public Task Handle(ClientRegisteredEvent notif, CancellationToken cancellationToken)
        {
            _bus.Publish(new PersonCreatedIntegrationEvent(notif.Person.Id, notif.Person.Name,
            notif.Person.IdentificationNumber, notif.Person.PostalCode,
            notif.Person.Place, notif.Person.Address));

            _bus.Publish(new ClientRegisteredIntegrationEvent(notif.Id, notif.PersonId,
            notif.Person, notif.Username, notif.Email,
            notif.Password, notif.PasswordHash, notif.PasswordSalt));

            return Task.CompletedTask;
        }

        public Task Handle(ClientLoginEvent notif, CancellationToken cancellationToken)
        {
            _bus.Publish(new ClientLoginIntegrationEvent(notif.Username, notif.Password));
            return Task.CompletedTask;
        }

        public Task Handle(ClientUpdatedEvent notif, CancellationToken cancellationToken)
        {
            _bus.Publish(new PersonUpdatedIntegrationEvent(notif.Person.Id, notif.Person.Name,
            notif.Person.IdentificationNumber, notif.Person.PostalCode,
            notif.Person.Place, notif.Person.Address));

            _bus.Publish(new ClientUpdatedIntegrationEvent(notif.Id, notif.PersonId,
            notif.Person, notif.Username, notif.Email,
            notif.Password, notif.PasswordHash, notif.PasswordSalt));

            return Task.CompletedTask;
        }

        public Task Handle(ClientRemovedEvent notif, CancellationToken cancellationToken)
        {
            _bus.Publish(new ClientRemovedIntegrationEvent(notif.Id));
            return Task.CompletedTask;
        }
    }
}
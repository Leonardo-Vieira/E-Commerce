using System.Threading.Tasks;
using e_order.Repository;
using e_order.Domain.IntegrationEvents.Client;
using e_order.Domain.Models;
using Domain_Core.Events;
using Newtonsoft.Json;
using Domain_Core.Bus;
using System;

namespace e_order.Domain.IntegrationEventHandlers
{
    public class ClientIntegrationEventHandler : IEventHandler<ClientRegisteredIntegrationEvent>, IEventHandler<ClientRemovedIntegrationEvent>, IEventHandler<ClientUpdatedIntegrationEvent>
    {
        private readonly IClientRepository _repo;
        private readonly IEventStoreRepository _eventStore;

        public ClientIntegrationEventHandler(IClientRepository repo, IEventStoreRepository eventStore)
        {
            _eventStore = eventStore;
            _repo = repo;
        }

        public Task Handle(ClientRegisteredIntegrationEvent integrEvent)
        {
            var newClient = new Client(integrEvent.Id,integrEvent.Username,integrEvent.Email,integrEvent.Name,integrEvent.IdentificationNumber,integrEvent.PostalCode,integrEvent.Place,integrEvent.Address,integrEvent.IsCollaborator);
            _repo.Create(newClient);
            _repo.Save();
            _eventStore.Store(new StoredEvent(integrEvent, JsonConvert.SerializeObject(integrEvent)));
            return Task.CompletedTask;
        }

        public Task Handle(ClientRemovedIntegrationEvent @event)
        {
            _repo.Remove(@event.Id);
            _repo.Save();
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            return Task.CompletedTask;
        }

        public Task Handle(ClientUpdatedIntegrationEvent @event)
        {
          /*   var updateClient = new Client(@event.Id, @event.Person.Id, @event.Username, 
            @event.Password, @event.Email, @event.PasswordHash, @event.PasswordSalt);
            _repo.Update(updateClient);
 */
 
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            return Task.CompletedTask;
        }
    }
}
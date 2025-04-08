/* using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.Events;
using e_order.Domain.IntegrationEvents.Client;
using e_order.Domain.IntegrationEvents.Person;
using e_order.Domain.Models;
using e_order.Repository;
using Newtonsoft.Json;

namespace e_order.Domain.IntegrationEventHandlers
{
    public class PersonIntegrationEventHandler : IEventHandler<PersonCreatedIntegrationEvent>, IEventHandler<PersonRemovedIntegrationEvent>, IEventHandler<PersonUpdatedIntegrationEvent>
    {
        private readonly IPersonRepository _repo;
        private readonly IEventStoreRepository _eventStore;

        public PersonIntegrationEventHandler(IPersonRepository repo, IEventStoreRepository eventStore)
        {
            _eventStore = eventStore;
            _repo = repo;
        }

        public Task Handle(PersonCreatedIntegrationEvent @event)
        {
            var newPerson = new Person(@event.Id, @event.Name, @event.IdentificationNumber, @event.PostalCode, @event.Place, @event.Address);
            _repo.Create(newPerson);
            _repo.Save();
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            return Task.CompletedTask;
        }

        public Task Handle(PersonRemovedIntegrationEvent @event)
        {
            _repo.Remove(@event.Id);
            _repo.Save();
             _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            
            return Task.CompletedTask;
        }

        public Task Handle(PersonUpdatedIntegrationEvent @event)
        {
            var updatedPerson = new Person(@event.Id, @event.Name, @event.IdentificationNumber, @event.PostalCode, @event.Place, @event.Address);
            _repo.Update(updatedPerson);
            _repo.Save();
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));

            return Task.CompletedTask;
        }
    }
} */
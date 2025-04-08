using System.Threading.Tasks;
using Domain_Core.Commands;
using Domain_Core.Events;
using MediatR;
using Newtonsoft.Json;

namespace Domain_Core.Bus {
    public class InMemoryBus : IMediatorHandler {
        private readonly IMediator _mediator;
        private readonly IEventStoreRepository _eventStore;

        public InMemoryBus (IMediator mediator, IEventStoreRepository eventStore) {
            _eventStore = eventStore;
            _mediator = mediator;
        }
        public Task SendCommand<T> (T command) where T : Command {
            return _mediator.Send (command);
        }
        public Task RaiseEvent<T> (T @event) where T : Event {
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            return _mediator.Publish (@event);
        }
       /*  public Task RaiseEvent<T> (T @event) where T : Event {
            _eventStore.Store(new StoredEvent(@event, JsonConvert.SerializeObject(@event)));
            return _mediator.Publish (@event);
        } */
    }
}
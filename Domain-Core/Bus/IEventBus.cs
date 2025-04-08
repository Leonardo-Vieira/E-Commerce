using Domain_Core.Events;
using System;

namespace Domain_Core.Bus
{
    public interface IEventBus : IEventHandler
    {
        void Publish(IntegrationEvent @event);
        void Consume(string destination);
        void Subscribe<TEvent, TEventHandler>()
        where TEvent : IntegrationEvent
        where TEventHandler : IEventHandler; 
        void Unsubscribe<TEvent, TEventHandler>()
        where TEvent : IntegrationEvent
        where TEventHandler : IEventHandler;
    }
}
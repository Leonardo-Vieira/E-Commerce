using System;
using System.Threading.Tasks;

namespace Domain_Core.Events
{
    public interface IEventHandler<TIntegrationEvent> : IEventHandler where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }
    public interface IEventHandler
    {
    }

}
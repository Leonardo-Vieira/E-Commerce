using System;
using MediatR;

namespace Domain_Core.Events
{
    public abstract class IntegrationEvent : Message, INotification
    {
       public DateTime Timestamp { get; private set; }

        protected IntegrationEvent()
        {
            Timestamp = DateTime.Now;
        } 
    }
}
using System;
using MediatR;

namespace Domain_Core.Events 
{
    public class Message : IRequest 
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message () 
        {
            MessageType = GetType().Name;
        }
    }
}
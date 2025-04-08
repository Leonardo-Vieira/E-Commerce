using System;
using System.Threading;
using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.Events;
using MediatR;

namespace Authentication.Events
{
    public class ClientRegisteredEvent : Event
    {
        public ClientRegisteredEvent(string id, string username, string email, string name, string identificationNumber, string postalCode, string place, string address, bool isCollaborator)
        {
            Id = id;
            Username = username;
            Email = email;
            Name = name;
            IdentificationNumber = identificationNumber;
            PostalCode = postalCode;
            Place = place;
            Address = address;
            IsCollaborator = isCollaborator;
        }

        public string Id {get;set;}
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string PostalCode { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public bool IsCollaborator { get; set; }

        
    }

    public class ClientEventHandler : INotificationHandler<ClientRegisteredEvent>
    {

        private readonly IEventBus _bus;

        public ClientEventHandler (IEventBus bus) {
            _bus = bus;
        }
        public Task Handle(ClientRegisteredEvent notif, CancellationToken cancellationToken)
        {
             _bus.Publish(new ClientRegisteredIntegrationEvent(notif.Id, notif.Username, notif.Email, notif.Name, notif.IdentificationNumber, notif.PostalCode, notif.Place, notif.Address, notif.IsCollaborator));
            return Task.CompletedTask;
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using Domain_Core.Events;

namespace Authentication.Events
{
    public class ClientRegisteredIntegrationEvent : IntegrationEvent
    {
        public ClientRegisteredIntegrationEvent(string id, string username, string email, string name, string identificationNumber, string postalCode, string place, string address, bool isCollaborator)
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
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string PostalCode { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public bool IsCollaborator { get; set; }
    }

}
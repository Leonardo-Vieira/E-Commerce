using Domain_Core.Events;

namespace E_Client.Domain.IntegrationEvents.Client
{
    public class ClientLoginIntegrationEvent : IntegrationEvent
    {
        public ClientLoginIntegrationEvent(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public string Username { get; protected set; }
        public string Password { get; protected set; }
    }
}
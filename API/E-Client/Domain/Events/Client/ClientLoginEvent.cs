using Domain_Core.Events;

namespace E_Client.Domain.Events.Client
{
    public class ClientLoginEvent : Event
    {
        public ClientLoginEvent(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
using System;
using E_Client.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Client.Domain.Commands.Client
{
    public class LoginClientCommand : ClientCommand
    {
        private readonly DataContext _context;
        public LoginClientCommand(Guid id, string username, string password, DataContext context)
        {
            Id = id;
            Username = username;
            Password = password;
            _context = context;
        }
        public override bool IsValid()
        {
            try
            {
                var clientId = _context.Clients.FirstOrDefaultAsync(x => x.Username == Username).Result.Id;
                var verifyClient = _context.Clients.Find(clientId);

                if (verifyClient != null && !String.IsNullOrWhiteSpace(verifyClient.Id.ToString()))
                {
                    using (var hmac = new System.Security.Cryptography.HMACSHA512(verifyClient.PasswordSalt))
                    {
                        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                        for (int i = 0; i < computedHash.Length; i++)
                        {
                            if (computedHash[i] != verifyClient.PasswordHash[i])
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
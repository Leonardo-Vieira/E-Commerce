using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using E_Client.Models;
using E_Client.Data;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using E_Client.Repository;
using Domain_Core.Bus;
using E_Client.Domain.Commands.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System.Security.Principal;

namespace E_Client.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository<Client> _clientRepo;
        private readonly IConfiguration _config;
        private readonly DataContext _context;
        private readonly IMediatorHandler _bus;
        private readonly TokenConfigurations _tokenConfigurations;
        private readonly SigningConfigurations _signingConfigurations;

        public ClientController(SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations, IClientRepository<Client> clientRepo, IConfiguration config, DataContext context, IMediatorHandler bus)
        {
            _bus = bus;
            _clientRepo = clientRepo;
            _config = config;
            _context = context;
            _tokenConfigurations = tokenConfigurations;
            _signingConfigurations = signingConfigurations;
        }

        [HttpGet]
        public async Task<IActionResult> GetClient()
        {
            if (ModelState.IsValid)
            {
                var clients = await _clientRepo.GetAll();
                return Ok(clients);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (ModelState.IsValid)
            {
                var client = await _clientRepo.GetByClientId(id);
                return Ok(client);
            }
            return BadRequest("Cliente não encontrado");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(Client client)
        {
            if (ModelState.IsValid)
            {
                client.Username.ToLower();
                var loginCommand = new LoginClientCommand(client.Id, client.Username, client.Password, _context);
               // if (loginCommand.IsValid())
                {
                    _bus.SendCommand(loginCommand);
                    return Ok(GenerateJwtToken(loginCommand));
                }
            }
            return BadRequest(new
            {
                cliententicated = false,
                message = "Falha ao autenticar"
            });
        }

        private object GenerateJwtToken(LoginClientCommand client)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
            new GenericIdentity(client.Id.ToString(), "Login"),
                new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                    new Claim(JwtRegisteredClaimNames.UniqueName, client.Username)
                }
            );

            DateTime creationDate = DateTime.Now;
            DateTime expiryDate = creationDate +
                TimeSpan.FromHours(_tokenConfigurations.Hour);

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expiryDate
            });

            var token = tokenHandler.WriteToken(tokenDescriptor);

            return new
            {
                cliententicated = true,
                created = creationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expiryDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(Client client)
        {
            if (ModelState.IsValid && client != null)
            {
                client.Username = client.Username.ToLower();

                var registerCommand = new RegisterClientCommand(client.PersonId, client.Person,
                client.Username, client.Email, client.Password, client.PasswordHash, client.PasswordSalt, _context);

                if (registerCommand.IsValid())
                {
                    _bus.SendCommand(registerCommand);
                    return Ok();
                }
            }
            return BadRequest("Cliente já existe ou dados incorretos!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(Client client, Guid id)
        {
            if (ModelState.IsValid)
            {
                client.Username = client.Username.ToLower();

                if (id != null)
                {
                    client.Person.Id = _context.Clients.FirstOrDefaultAsync(x => x.Id == id).Result.PersonId;

                    var updateCommand = new UpdateClientCommand(id, client.PersonId, client.Person, client.Username,
                     client.Email, client.Password, client.PasswordHash, client.PasswordSalt, _context);

                    if (updateCommand.IsValid())
                    {
                        _bus.SendCommand(updateCommand);
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                var deleteCommand = new RemoveClientCommand(id, _context);
                if (deleteCommand.IsValid())
                {
                    _bus.SendCommand(deleteCommand);
                    return Ok();
                }
            }
            return BadRequest("Cliente não encontrado!");
        }
    }
}
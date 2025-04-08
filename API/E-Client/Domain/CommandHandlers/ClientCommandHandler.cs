using System;
using System.Threading;
using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.CommandHandlers;
using E_Client.Models;
using E_Client.Repository;
using MediatR;
using E_Client.Domain.Commands.Client;
using E_Client.Domain.Events.Client;

namespace E_Client.Domain.CommandHandlers
{
    public class ClientCommandHandler : CommandHandler, IRequestHandler<RegisterClientCommand>, IRequestHandler<LoginClientCommand>, IRequestHandler<RemoveClientCommand>, IRequestHandler<UpdateClientCommand>
    {
        private readonly IClientRepository<Client> _repo;
        private readonly IMediatorHandler _bus;
        public ClientCommandHandler(IClientRepository<Client> repo, IMediatorHandler bus) : base(bus)
        {
            _bus = bus;
            _repo = repo;
        }

        public Task Handle(RegisterClientCommand request, CancellationToken cancellationToken)
        {
            var r = new Client(Guid.NewGuid(), Guid.NewGuid(), request.Person, request.Username,
             request.Email, request.Password, request.PasswordHash, request.PasswordSalt);

            _repo.Register(r);

            _bus.RaiseEvent(new ClientRegisteredEvent(r.Id, r.PersonId, r.Person, r.Username, r.Email, r.Password,
            r.PasswordHash, r.PasswordSalt));
            return Task.CompletedTask;
        }

        public Task Handle(LoginClientCommand request, CancellationToken cancellationToken)
        {
            _bus.RaiseEvent(new ClientLoginEvent(request.Username, request.Password));
            return Task.CompletedTask;
        }

        public Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var r = new Client(request.Id, request.PersonId, request.Person, request.Username,
             request.Email, request.Password, request.PasswordHash, request.PasswordSalt);

            _repo.Update(r);

            _bus.RaiseEvent(new ClientUpdatedEvent(r.Id, r.PersonId, r.Person, r.Username, r.Email, r.Password,
             r.PasswordHash, r.PasswordSalt
            ));
            return Task.CompletedTask;
        }

        public Task Handle(RemoveClientCommand request, CancellationToken cancellationToken)
        {
            _repo.Remove(request.Id);
            _bus.RaiseEvent(new ClientRemovedEvent(request.Id));
            return Task.CompletedTask;
        }
    }
}
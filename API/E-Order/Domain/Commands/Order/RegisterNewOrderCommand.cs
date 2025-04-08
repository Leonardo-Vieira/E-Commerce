using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using Domain_Core.CommandHandlers;
using e_order.Domain.Event.Order;
using e_order.Domain.Validations;
using e_order.Domain.Validations.Order;
using e_order.Repository;
using E_Order.Domain.Models.Dto;
using E_Order.Domain.Models;
using MediatR;

namespace e_order.Domain.Commands.Order {
    public class RegisterNewOrderCommand : OrderCommand {
        public RegisterNewOrderCommand (Guid clientId, OrderItemDto[] orderItems) {
            ClientId = clientId;
            DateOrder = DateTime.Now;
            State = true;
            OrderItems = orderItems;
        }

        public override bool IsValid () {
            ValidationResult = new RegisterNewOrderCommandValidation ().Validate (this);
            return ValidationResult.IsValid;
        }
    }
    public class RegisterNewOrderCommandHandler : CommandHandler, IRequestHandler<RegisterNewOrderCommand> {
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public RegisterNewOrderCommandHandler (IMediatorHandler bus, IMapper mapper, IOrderRepository orderRepository) : base (bus) {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _bus = bus;

        }
        public Task Handle (RegisterNewOrderCommand request, CancellationToken cancellationToken) {
            var orderItems = _mapper.Map<e_order.Domain.Models.OrderItem[]> (request.OrderItems);

            var order = new e_order.Domain.Models.Order (request.ClientId, orderItems, request.State);
            _orderRepository.Create (order);
            _orderRepository.Save ();
            _bus.RaiseEvent (new OrderCreatedEvent (order.Id, order.ClientId, order.OrderItems, order.DateOrder, order.State));

            return Task.CompletedTask;
        }
    }
}
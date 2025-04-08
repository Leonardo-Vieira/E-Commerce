using System;
using System.Threading;
using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.CommandHandlers;
using e_order.Domain.Event.Order;
using e_order.Domain.Models;
using e_order.Domain.Validations.Order;
using e_order.Repository;
using MediatR;

namespace e_order.Domain.Commands.Order
{
    public class CancelOrderCommand : OrderCommand
    {
        public CancelOrderCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new CancelOrderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CancelOrderCommandHandler : CommandHandler, IRequestHandler<CancelOrderCommand>
    {
        public IOrderRepository _orderRepository;
        public IOrderItemRepository _orderItemRepository;
        public IMediatorHandler _bus;
        public CancelOrderCommandHandler(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IMediatorHandler bus ):base(bus)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _bus = bus;
        }
        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetById (request.Id) ?? throw new ArgumentNullException ();
            order.OrderItems = _orderItemRepository.GetByOrderId(order.Id);

            _orderRepository.Cancel(order.Id);
            
            foreach (var item in order.OrderItems)
            {
                await _bus.RaiseEvent(new ProductOrderCancelledEvent(item.ProductId, item.Quantity));
            }
        }
    }
}
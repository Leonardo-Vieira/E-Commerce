using System;
using System.Threading;
using System.Threading.Tasks;
using Domain_Core.Bus;
using Domain_Core.CommandHandlers;
using e_order.Domain.Commands.OrderItem;
using e_order.Domain.Event.OrderItem;
using e_order.Domain.Models;
using e_order.Repository;
using MediatR;

namespace e_order.Domain.CommandHandlers
{
    public class OrderItemCommandHandler : 
    CommandHandler,
    IRequestHandler<RegisterNewOrderItemCommand>,
    IRequestHandler<UpdateOrderItemCommand>,
    IRequestHandler<RemoveOrderItemCommand>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMediatorHandler _bus;

        public OrderItemCommandHandler(IOrderItemRepository OrderItemRepository, IMediatorHandler bus) : base(bus)
        {
            _orderItemRepository = OrderItemRepository;
            _bus = bus;
        }

        public Task Handle(RegisterNewOrderItemCommand message, CancellationToken cancellationToken)
        {
            var orderItem = new OrderItem(message.Quantity, message.ProductId.Value);
            _orderItemRepository.Create(orderItem);
            _orderItemRepository.Save();
            _bus.RaiseEvent(new ProductOrderRequestedEvent(orderItem.ProductId, orderItem.Quantity));

            return Task.CompletedTask;
        }

        public Task Handle(UpdateOrderItemCommand message, CancellationToken cancellationToken)
        {
            var orderItem = new OrderItem(message.Quantity, message.ProductId.Value);
            _orderItemRepository.Update(orderItem);
              _orderItemRepository.Save();
            _bus.RaiseEvent(new OrderItemUpdateEvent(orderItem.Id, orderItem.ProductId, orderItem.Quantity));

            return Task.CompletedTask;
        }

        public Task Handle(RemoveOrderItemCommand message, CancellationToken cancellationToken)
        {
            _orderItemRepository.Remove(message.Id);
              _orderItemRepository.Save();
            _bus.RaiseEvent(new ProductOrderRequestedEvent(message.Id, message.Quantity));
            
            return Task.CompletedTask;
        }
    }
}
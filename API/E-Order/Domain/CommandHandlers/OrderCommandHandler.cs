using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain_Core.Bus;
using Domain_Core.CommandHandlers;
using e_order.Domain.Commands.Order;
using e_order.Domain.Commands.OrderItem;
using e_order.Domain.Event.Order;
using e_order.Domain.Models;
using e_order.Repository;
using MediatR;

namespace e_order.Domain.CommandHandlers {
    public class OrderCommandHandler:
        CommandHandler,
        IRequestHandler<RegisterNewOrderCommand>,
        IRequestHandler<UpdateOrderCommand>,
        IRequestHandler<RemoveOrderCommand>,
        IRequestHandler<CancelOrderCommand> {
            private readonly IOrderRepository _orderRepository;
            private readonly IOrderItemRepository _orderItemRepository;
            private readonly IMediatorHandler _bus;
            private readonly IMapper _mapper;

            public OrderCommandHandler (IOrderRepository orderRepository, IOrderItemRepository orderItemRepository,
                IMediatorHandler bus, IMapper mapper) : base (bus) {
                _mapper = mapper;
                _orderRepository = orderRepository;
                _orderItemRepository = orderItemRepository;
                _bus = bus;
            }

            public Task Handle (RegisterNewOrderCommand message, CancellationToken cancellationToken) {
                /*  foreach (var orderItem in message.OrderItems)
                 {
                     _bus.SendCommand(new RegisterNewOrderItemCommand(orderItem.Quantity, orderItem.ProductId));
                 } */
                 var orderItems = _mapper.Map<OrderItem[]>(message.OrderItems); 

               //OrderItem[] orderItems ;


                var order = new Order (message.ClientId, orderItems, message.State);
                _orderRepository.Create (order);
                _orderRepository.Save ();
                _bus.RaiseEvent (new OrderCreatedEvent (order.Id, order.ClientId, order.OrderItems, order.DateOrder, order.State));

                return Task.CompletedTask;
            }

            public Task Handle (UpdateOrderCommand message, CancellationToken cancellationToken) {
                /*  var order = new Order(message.Id, message.ClientId, message.State, message.OrderItemId, message.OrderItem);
            _orderRepository.Update(order);
            _bus.RaiseEvent(new OrderUpdatedEvent(order.Id, order.ClientId, order.DateOrder, order.State, order.OrderItem, order.OrderItemId));
*/
                return Task.CompletedTask;
            }

            public Task Handle (RemoveOrderCommand message, CancellationToken cancellationToken) {
                _orderRepository.Remove (message.Id);
                _orderRepository.Save ();
                _bus.RaiseEvent (new OrderRemovedEvent (message.Id));

                return Task.CompletedTask;
            }

            public async Task Handle (CancelOrderCommand request, CancellationToken cancellationToken) {
               var order = await _orderRepository.GetById (request.Id) ??
                    throw new ArgumentNullException ();
             //   OrderItem orderItem = await _orderItemRepository.GetById(request.Id) ?? throw new ArgumentNullException();
                 order.OrderItems = _orderItemRepository.GetByOrderId(order.Id);
                 
                _orderRepository.Cancel(order.Id);
                foreach (var item in order.OrderItems)
                {
                    await _bus.RaiseEvent(new ProductOrderCancelledEvent(item.ProductId, item.Quantity));
                }

                //return Task.CompletedTask;
            }
        }
}
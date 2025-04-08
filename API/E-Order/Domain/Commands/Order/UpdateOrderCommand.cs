using System;
using e_order.Domain.Validations;
using e_order.Domain.Validations.Order;

namespace e_order.Domain.Commands.Order
{
    public class UpdateOrderCommand : OrderCommand
    {
        public UpdateOrderCommand(Guid id, Guid clientId, e_order.Domain.Models.OrderItem orderItem, DateTime dateOrder, bool state, Guid? orderItemId)
        {
           /*  Id = id;
            OrderItem = orderItem;
            ClientId = clientId;
            DateOrder = dateOrder;
            State = state;
            OrderItemId = orderItemId; */
        }
        public override bool IsValid()
        {
            ValidationResult = new UpdateOrderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
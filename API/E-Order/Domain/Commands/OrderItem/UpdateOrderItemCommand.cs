using System;
using e_order.Domain.Validations;
using e_order.Domain.Validations.OrderItem;

namespace e_order.Domain.Commands.OrderItem
{
    public class UpdateOrderItemCommand : OrderItemCommand
    {
        public UpdateOrderItemCommand(Guid id, Guid productId, int quantity)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
        }
        public override bool IsValid()
        {
            ValidationResult = new UpdateOrderItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
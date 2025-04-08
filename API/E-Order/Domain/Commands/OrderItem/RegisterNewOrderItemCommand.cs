using System;
using e_order.Domain.Validations.OrderItem;

namespace e_order.Domain.Commands.OrderItem
{
    public class RegisterNewOrderItemCommand : OrderItemCommand
    {
        public RegisterNewOrderItemCommand(int quantity, Guid? productId)
        {
            Quantity = quantity;
            ProductId = productId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterNewOrderItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
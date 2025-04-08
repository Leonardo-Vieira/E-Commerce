using System;
using e_order.Domain.Validations;
using e_order.Domain.Validations.OrderItem;

namespace e_order.Domain.Commands.OrderItem
{
    public class RemoveOrderItemCommand : OrderItemCommand
    {
        public RemoveOrderItemCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveOrderItemCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
using System;
using e_order.Domain.Validations;
using e_order.Domain.Validations.Order;

namespace e_order.Domain.Commands.Order
{
    public class RemoveOrderCommand : OrderCommand
    {
        public RemoveOrderCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveOrderCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
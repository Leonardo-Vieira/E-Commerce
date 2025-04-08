using System;
using e_order.Domain.Commands.OrderItem;
using FluentValidation;

namespace e_order.Domain.Validations.OrderItem
{
    public abstract class OrderItemValidation<T> : AbstractValidator<T> where T : OrderItemCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
        }

        protected void ValidateProductId()
        {
            RuleFor(c => c.ProductId)
            .Equals(Guid.Empty);
        }

        protected void ValidateQuantity()
        {
            RuleFor(c => c.Quantity)
            .NotEmpty();
        }
    }
}
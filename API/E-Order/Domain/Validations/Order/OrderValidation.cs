using System;
using e_order.Domain.Commands.Order;
using FluentValidation;

namespace e_order.Domain.Validations.Order
{
    public abstract class OrderValidation<T> : AbstractValidator<T> where T : OrderCommand
    {
        protected void ValidateId()
        {
            RuleFor(c => c.Id).NotEqual(Guid.Empty);
        }

        protected void ValidateClientId()
        {
            RuleFor(c => c.ClientId).NotEmpty();
        }

        protected void ValidateDateOrder()
        {
            RuleFor(c => c.DateOrder).NotEmpty();
        }

        protected void ValidateState()
        {
            RuleFor(c => c.State).NotEmpty();
        }
    }
}
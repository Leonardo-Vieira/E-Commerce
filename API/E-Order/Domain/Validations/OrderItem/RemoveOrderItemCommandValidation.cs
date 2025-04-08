using e_order.Domain.Commands.OrderItem;

namespace e_order.Domain.Validations.OrderItem
{
    public class RemoveOrderItemCommandValidation : OrderItemValidation<RemoveOrderItemCommand>
    {
        public RemoveOrderItemCommandValidation()
        {
            ValidateId();
        }
    }
}
using e_order.Domain.Commands.OrderItem;

namespace e_order.Domain.Validations.OrderItem
{
    public class UpdateOrderItemCommandValidation : OrderItemValidation<UpdateOrderItemCommand>
    {
        public UpdateOrderItemCommandValidation()
        {
            ValidateId();
            ValidateProductId();
            ValidateQuantity();
        }
    }
}
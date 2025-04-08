using e_order.Domain.Commands.OrderItem;

namespace e_order.Domain.Validations.OrderItem
{
    public class RegisterNewOrderItemCommandValidation : OrderItemValidation<RegisterNewOrderItemCommand>
    {
        public RegisterNewOrderItemCommandValidation()
        {
            ValidateProductId();
            ValidateQuantity();
        }
    }
}
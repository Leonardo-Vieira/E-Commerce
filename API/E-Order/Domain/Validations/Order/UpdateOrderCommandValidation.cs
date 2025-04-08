using e_order.Domain.Commands.Order;

namespace e_order.Domain.Validations.Order
{
    public class UpdateOrderCommandValidation : OrderValidation<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidation()
        {
            ValidateClientId();
            ValidateId();
            ValidateDateOrder();
            ValidateState();
        }
    }
}
using e_order.Domain.Commands.Order;

namespace e_order.Domain.Validations.Order
{
    public class RegisterNewOrderCommandValidation : OrderValidation<RegisterNewOrderCommand>
    {
        public RegisterNewOrderCommandValidation()
        {
            ValidateClientId();
            ValidateDateOrder();
            ValidateState();
        }
    }
}
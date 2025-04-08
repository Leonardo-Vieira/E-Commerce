using e_order.Domain.Commands.Order;

namespace e_order.Domain.Validations.Order
{
    public class CancelOrderCommandValidation : OrderValidation<CancelOrderCommand>
    {
        public CancelOrderCommandValidation()
        {
            ValidateClientId();
        }
    }
}
using e_order.Domain.Commands.Order;

namespace e_order.Domain.Validations.Order
{
    public class RemoveOrderCommandValidation : OrderValidation<RemoveOrderCommand>
    {
        public RemoveOrderCommandValidation()
        {
            ValidateId();
        }
    }
}
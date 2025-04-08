using Domain_Core.Bus;
using Domain_Core.Events;

namespace Domain_Core.CommandHandlers {
    public class CommandHandler {
        private readonly IMediatorHandler _bus;
        public CommandHandler (IMediatorHandler bus) {
            _bus = bus;
        }

    }
}
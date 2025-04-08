using System.Threading.Tasks;
using Domain_Core.Commands;
using Domain_Core.Events;

namespace Domain_Core.Bus
{
    public interface IMediatorHandler
    {
         Task SendCommand<T>(T command) where T: Command;

         Task RaiseEvent<T>(T @event) where T : Event;
    }
}
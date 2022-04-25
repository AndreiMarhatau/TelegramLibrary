using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models.ArgsForEvents;

namespace TelegramLibrary.Builders
{
    public interface IConnectionLimiterBuilder
    {
        IConnectionLimiterBuilder LimitConnectionsByUserWhileHandling();
        IConnectionLimiterBuilder LimitConnectionsByUserAfterHandling(TimeSpan timeSpan);
        IConnectionLimiterBuilder HandleOnRelease(EventHandler<ControlHandlingEventArgs> handler);
        ITelegramServiceBuilder SaveLimiter();
    }
}

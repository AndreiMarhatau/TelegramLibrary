using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramLibrary.Builders
{
    public interface IConnectionLimiterBuilder
    {
        IConnectionLimiterBuilder LimitConnectionsByUserWhileHandling();
        IConnectionLimiterBuilder LimitConnectionsByUserAfterHandling(TimeSpan timeSpan);
        ITelegramServiceBuilder SaveLimiter();
    }
}

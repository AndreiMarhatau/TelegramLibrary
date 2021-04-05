using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;
using TelegramLibrary.Models.ArgsForEvents;

namespace TelegramLibrary.Builders
{
    public class ConnectionLimiterBuilder : IConnectionLimiterBuilder
    {
        private ITelegramServicePropertiesSaver _builder;
        private TimeSpan _delay;
        private EventHandler<ControlHandlingEventArgs> _onRelease;

        public ConnectionLimiterBuilder(ITelegramServicePropertiesSaver builder)
        {
            this._builder = builder;
        }

        public IConnectionLimiterBuilder LimitConnectionsByUserWhileHandling()
        {
            this._delay = TimeSpan.Zero;
            return this;
        }

        public IConnectionLimiterBuilder LimitConnectionsByUserAfterHandling(TimeSpan delay)
        {
            this._delay = delay;
            return this;
        }

        public IConnectionLimiterBuilder HandleOnRelease(EventHandler<ControlHandlingEventArgs> handler)
        {
            this._onRelease = handler;
            return this;
        }

        public ITelegramServiceBuilder SaveLimiter()
        {
            IConnectionLimiter limiter = new ConnectionLimiter(_delay, _onRelease);
            return this._builder.SaveLimiter(limiter);
        }
    }
}

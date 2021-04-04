using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Builders
{
    public class ConnectionLimiterBuilder : IConnectionLimiterBuilder
    {
        private ITelegramServicePropertiesSaver _telegramServiceBuilder;
        private TimeSpan _delay;

        public ConnectionLimiterBuilder(ITelegramServicePropertiesSaver telegramServiceBuilder)
        {
            this._telegramServiceBuilder = telegramServiceBuilder;
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

        public ITelegramServiceBuilder SaveLimiter()
        {
            IConnectionLimiter limiter = new ConnectionLimiter(_delay);
            return this._telegramServiceBuilder.SaveLimiter(limiter);
        }
    }
}

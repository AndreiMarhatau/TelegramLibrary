using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models.ArgsForEvents;
using TelegramLibrary.TelegramInteraction;

namespace TelegramLibrary.Models
{
    internal class ConnectionLimiter : IConnectionLimiter
    {
        private TimeSpan _delay;
        private EventHandler<ControlHandlingEventArgs> _onRelease;
        private List<long> _capturedUsers = new List<long>();
        private object _lock = new object();

        public ConnectionLimiter(TimeSpan delay, EventHandler<ArgsForEvents.ControlHandlingEventArgs> onReleaseLimiterHandler)
        {
            this._delay = delay;
            this._onRelease = onReleaseLimiterHandler;
        }

        public bool TryCapture(long id)
        {
            if (!_capturedUsers.Any(userId => userId == id))
            {
                lock (_lock)
                {
                    if (!_capturedUsers.Any(userId => userId == id))
                    {
                        _capturedUsers.Add(id);
                        return true;
                    }
                }
            }
            return false;
        }

        public void Release(long id, ITelegramInteractor telegramInteractor)
        {
            Task.Run(async () =>
            {
                await Task.Delay(_delay);
                lock (_lock)
                {
                    _capturedUsers.Remove(id);
                }
                if(_onRelease != null) _onRelease(this, new ControlHandlingEventArgs(telegramInteractor));
            });
        }
    }
}

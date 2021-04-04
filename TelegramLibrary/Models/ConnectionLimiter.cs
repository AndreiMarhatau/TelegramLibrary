using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramLibrary.Models
{
    internal class ConnectionLimiter : IConnectionLimiter
    {
        private TimeSpan _delay;
        private List<int> _capturedUsers = new List<int>();
        private object _lock = new object();

        public ConnectionLimiter(TimeSpan delay)
        {
            this._delay = delay;
        }

        public bool TryCapture(int id)
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

        public void Release(int id)
        {
            Task.Run(async () =>
            {
                await Task.Delay(_delay);
                lock (_lock)
                {
                    _capturedUsers.Remove(id);
                }
            });
        }
    }
}

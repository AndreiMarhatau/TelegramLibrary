using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.TelegramInteraction;

namespace TelegramLibrary.Models
{
    public interface IConnectionLimiter
    {
        bool TryCapture(long id);
        void Release(long id, ITelegramInteractor telegramInteractor);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Models.ArgsForEvents;
using TelegramLibrary.TelegramInteraction;

namespace TelegramLibrary.Models
{
    public abstract class ControlBase: ILimitable
    {
        IConnectionLimiter ILimitable.Limiter { get; set; }

        internal event EventHandler<ControlHandlingEventArgs> HandleEvent;

        internal abstract bool IsAbleToProceed(Update update);

        internal void Handle(ITelegramInteractor telegramInteractor)
        {
            if((this as ILimitable).Limiter?.TryCapture(telegramInteractor.User.Id) == false)
            {
                return;
            }
            try
            {
                HandleEvent(this, new ControlHandlingEventArgs(telegramInteractor));
            }
            finally
            {
                (this as ILimitable).Limiter?.Release(telegramInteractor.User.Id);
            }
        }
    }
}

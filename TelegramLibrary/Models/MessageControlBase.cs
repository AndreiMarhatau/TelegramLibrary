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
    public abstract class MessageControlBase
    {
        internal event EventHandler<ControlHandlingEventArgs> HandleEvent;

        internal abstract bool IsAbleToProceed(Update update);

        internal void Handle(ITelegramInteractor telegramInteractor)
        {
            HandleEvent(this, new ControlHandlingEventArgs(telegramInteractor));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Extensions;
using TelegramLibrary.Models.ArgsForEvents;

namespace TelegramLibrary.Models.MessageControls
{
    internal class StartCommand : MainControlBase, ISingleControl
    {
        internal StartCommand(EventHandler<ControlHandlingEventArgs> handler)
        {
            this.HandleEvent += handler;
        }

        internal override bool IsAbleToProceed(Update update)
        {
            return update.Type == Telegram.Bot.Types.Enums.UpdateType.Message && update.GetMessage().Text.Equals("/start", StringComparison.OrdinalIgnoreCase);
        }
    }
}

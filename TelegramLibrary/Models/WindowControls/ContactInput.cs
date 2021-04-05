using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Extensions;

namespace TelegramLibrary.Models.WindowControls
{
    public class ContactInput : WindowControlBase, ISingleControl
    {
        internal override bool IsAbleToProceed(Update update)
        {
            return update.Type == Telegram.Bot.Types.Enums.UpdateType.Message &&
                update.GetMessage().Type == Telegram.Bot.Types.Enums.MessageType.Contact;
        }
    }
}

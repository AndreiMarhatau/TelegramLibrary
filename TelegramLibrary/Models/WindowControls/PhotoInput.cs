using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Extensions;

namespace TelegramLibrary.Models.WindowControls
{
    public class PhotoInput : WindowControlBase, ISingleControl
    {
        internal override bool IsAbleToProceed(Update update)
        {
            return update.GetMessage().Type == Telegram.Bot.Types.Enums.MessageType.Photo;
        }
    }
}

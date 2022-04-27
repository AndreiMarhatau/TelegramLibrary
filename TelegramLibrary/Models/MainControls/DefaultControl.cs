using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramLibrary.Models.MainControls
{
    internal class DefaultControl : ControlBase
    {
        internal override bool IsAbleToProceed(Update update)
        {
            return true;
        }
    }
}

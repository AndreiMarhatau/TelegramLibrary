﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Extensions;

namespace TelegramLibrary.Models.MainControls
{
    public class TextInput : MainControlBase, ISingleControl
    {
        internal override bool IsAbleToProceed(Update update)
        {
            return update.GetMessage().Type == Telegram.Bot.Types.Enums.MessageType.Text && !string.IsNullOrEmpty(update.GetMessage().Text.Trim());
        }
    }
}
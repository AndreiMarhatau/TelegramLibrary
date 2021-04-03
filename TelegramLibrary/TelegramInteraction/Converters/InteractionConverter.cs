using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramLibrary.TelegramInteraction.Converters
{
    internal static class InteractionConverter
    {
        internal static KeyboardButton ToTelegramControl(this Models.WindowControls.KeyboardButton button)
        {
            return new KeyboardButton(button.Name);
        }

        internal static InlineKeyboardButton ToTelegramControl(this Models.WindowControls.CallbackButton button)
        {
            return new InlineKeyboardButton() { Text = button.Text, CallbackData = button.Data };
        }
    }
}

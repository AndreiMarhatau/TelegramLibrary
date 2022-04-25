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
            return new KeyboardButton(button.Name) { RequestContact = button.RequestPhoneNumber, RequestLocation = button.RequestLocation };
        }

        internal static InlineKeyboardButton ToTelegramControl(this Models.WindowControls.CallbackButton button)
        {
            return new InlineKeyboardButton(button.Text) { CallbackData = button.Data };
        }
    }
}

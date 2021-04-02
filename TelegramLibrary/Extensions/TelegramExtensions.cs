using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramLibrary.Extensions
{
    public static class TelegramExtensions
    {
        public static Message GetMessage(this Update update)
        {
            return update.Message ?? update.CallbackQuery.Message;
        }

        public static User GetFrom(this Update update)
        {
            return update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery ? update.CallbackQuery.From : update.GetMessage().From;
        }
    }
}

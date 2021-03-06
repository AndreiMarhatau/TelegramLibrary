using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Models;

namespace TelegramLibrary.TelegramInteraction
{
    public interface ITelegramInteractor
    {
        Telegram.Bot.Types.Message Message { get; }
        UserModel User { get; }
        Telegram.Bot.ITelegramBotClient TelegramBotClient { get; }

        Task SendWindow(string name);
        Task SendText(string text);
        Task SendStartWindow();
        Task DeleteKeyboard(string text);
    }
}

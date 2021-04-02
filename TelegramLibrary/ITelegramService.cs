using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Models;

namespace TelegramLibrary
{
    public interface ITelegramService
    {
        public void RegisterWindows(params WindowBase[] windows);
        public Task HandleUpdate(Update update);
    }
}

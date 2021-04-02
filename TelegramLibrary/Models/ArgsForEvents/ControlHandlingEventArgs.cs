using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.TelegramInteraction;

namespace TelegramLibrary.Models.ArgsForEvents
{
    public class ControlHandlingEventArgs : EventArgs
    {
        private ITelegramInteractor _telegramInteractor;

        public ITelegramInteractor TelegramInteractor => _telegramInteractor;

        public ControlHandlingEventArgs(ITelegramInteractor telegramInteractor)
        {
            this._telegramInteractor = telegramInteractor;
        }
    }
}

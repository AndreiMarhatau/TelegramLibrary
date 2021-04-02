using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Models.ArgsForEvents;

namespace TelegramLibrary.Models.MessageControls
{
    public class KeyboardButton : MessageControlBase, IPositionalControl
    {
        private string _name;

        internal string Name => _name;

        internal KeyboardButton(string name)
        {
            this._name = name;
        }

        internal override bool IsAbleToProceed(Update update)
        {
            return update.Type == Telegram.Bot.Types.Enums.UpdateType.Message && update.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text && update.Message.Text == _name;
        }
    }
}

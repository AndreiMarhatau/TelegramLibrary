using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Extensions;

namespace TelegramLibrary.Models.WindowControls
{
    public class CallbackButton : MessageControlBase, IPositionalControl
    {
        private string _text;
        private string _data;

        public string Text => _text;
        public string Data => _data;

        public CallbackButton(string text, string data)
        {
            this._text = text;
            this._data = data;
        }

        internal override bool IsAbleToProceed(Update update)
        {
            return update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery &&
                update.CallbackQuery.Data == _data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Builders
{
    public class TelegramMessageBuilder : ITelegramMessageBuilder
    {
        private string _text;
        private ITelegramServiceBuilder _telegramServiceBuilder;
        private Message _message;

        internal TelegramMessageBuilder(ITelegramServiceBuilder telegramServiceBuilder, string text)
        {
            this._text = text;
            this._telegramServiceBuilder = telegramServiceBuilder;
            this._message = new Message() { Text = _text };
        }

        public IMessageKeyboardControlBuilder UseKeyboardControls()
        {
            return new MessageKeyboardControlBuilder(this);
        }

        public IMessageCallbackControlBuilder UseCallbackControls()
        {
            return new MessageCallbackControlBuilder(this);
        }

        ITelegramMessageBuilder ITelegramMessageBuilder.SaveControls(IEnumerable<IEnumerable<IPositionalControl>> positionalControls)
        {
            _message.PositionalControls = positionalControls;
            return this;
        }

        public ITelegramServiceBuilder SaveMessage()
        {
            return _telegramServiceBuilder.SaveMessage(this._message);
        }
    }
}

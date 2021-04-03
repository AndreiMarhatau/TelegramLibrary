using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Builders
{
    public class MessageBuilder : IMessageBuilder
    {
        private string _text;
        private IWindowBuilder _windowBuilder;
        private Message _message;

        internal MessageBuilder(IWindowBuilder windowBuilder, string text)
        {
            this._text = text;
            this._windowBuilder = windowBuilder;
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

        IMessageBuilder IMessageBuilder.SaveControls(IEnumerable<IEnumerable<IPositionalControl>> positionalControls)
        {
            _message.PositionalControls = positionalControls;
            return this;
        }

        public IWindowBuilder SaveMessage()
        {
            return _windowBuilder.SaveMessage(this._message);
        }
    }
}

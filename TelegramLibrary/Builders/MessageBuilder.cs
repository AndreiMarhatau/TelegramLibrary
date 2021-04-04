using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Builders
{
    public class MessageBuilder : IMessagePropertiesSaver
    {
        private IWindowPropertiesSaver _windowBuilder;
        private Message _message;

        internal MessageBuilder(IWindowPropertiesSaver windowBuilder)
        {
            this._windowBuilder = windowBuilder;
            this._message = new Message();
        }

        public IMessageKeyboardControlBuilder UseKeyboardControls()
        {
            return new MessageKeyboardControlBuilder(this);
        }

        public IMessageCallbackControlBuilder UseCallbackControls()
        {
            return new MessageCallbackControlBuilder(this);
        }

        IMessageBuilder IMessagePropertiesSaver.SaveControls(IEnumerable<IEnumerable<IPositionalControl>> positionalControls)
        {
            _message.PositionalControls = positionalControls;
            return this;
        }

        public IWindowBuilder SaveMessage()
        {
            if (String.IsNullOrEmpty(this._message.Text))
            {
                throw new InvalidOperationException("Message text must be fulfilled");
            }
            return _windowBuilder.SaveMessage(this._message);
        }

        public IMessageBuilder UseText(string text)
        {
            this._message.Text = text;
            return this;
        }

        public IMessageBuilder SetIsGlobalHandlers(bool isGlobal)
        {
            this._message.IsGlobal = isGlobal;
            return this;
        }
    }
}

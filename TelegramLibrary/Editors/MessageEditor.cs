using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Builders;
using TelegramLibrary.Models;

namespace TelegramLibrary.Editors
{
    public class MessageEditor : IMessagePropertiesSaver
    {
        private Message _message;

        public MessageEditor(Message message)
        {
            this._message = message;
        }

        public IMessageBuilder UseText(string text)
        {
            this._message.Text = text;
            return this;
        }

        IMessageBuilder IMessagePropertiesSaver.SaveControls(IEnumerable<IEnumerable<IPositionalControl>> positionalControls)
        {
            this._message.PositionalControls = positionalControls;
            return this;
        }

        public IWindowBuilder SaveMessage()
        {
            throw new InvalidOperationException("You shouldn't use this method through editor.");
        }

        public IMessageCallbackControlBuilder UseCallbackControls()
        {
            return new MessageCallbackControlBuilder(this);
        }

        public IMessageKeyboardControlBuilder UseKeyboardControls()
        {
            return new MessageKeyboardControlBuilder(this);
        }

        public IMessageBuilder SetIsGlobalHandlers(bool isGlobal)
        {
            this._message.IsGlobal = isGlobal;
            return this;
        }
    }
}

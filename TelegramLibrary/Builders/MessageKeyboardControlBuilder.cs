using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;
using TelegramLibrary.Models.ArgsForEvents;
using TelegramLibrary.Models.MessageControls;

namespace TelegramLibrary.Builders
{
    public class MessageKeyboardControlBuilder : IMessageKeyboardControlBuilder
    {
        private ITelegramMessageBuilder _telegramMessageBuilder;
        private List<List<IPositionalControl>> _positionalControls = new List<List<IPositionalControl>>();

        internal MessageKeyboardControlBuilder(ITelegramMessageBuilder telegramMessageBuilder)
        {
            this._telegramMessageBuilder = telegramMessageBuilder;
        }

        public IMessageKeyboardControlBuilder CreateRow()
        {
            _positionalControls.Add(new List<IPositionalControl>());
            return this;
        }

        public IMessageKeyboardControlBuilder UseKeyboardButtonControl(string text, EventHandler<ControlHandlingEventArgs> handler)
        {
            var control = new KeyboardButton(text);
            control.HandleEvent += handler;
            _positionalControls.Last().Add(control);
            return this;
        }

        public ITelegramMessageBuilder SaveControls()
        {
            return this._telegramMessageBuilder.SaveControls(_positionalControls);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;
using TelegramLibrary.Models.ArgsForEvents;
using TelegramLibrary.Models.WindowControls;

namespace TelegramLibrary.Builders
{
    public class MessageKeyboardControlBuilder : IMessageKeyboardControlBuilder
    {
        private IMessagePropertiesSaver _telegramMessageBuilder;
        private List<List<IPositionalControl>> _positionalControls = new List<List<IPositionalControl>>();

        internal MessageKeyboardControlBuilder(IMessagePropertiesSaver telegramMessageBuilder)
        {
            this._telegramMessageBuilder = telegramMessageBuilder;
        }

        public IMessageKeyboardControlBuilder CreateRow()
        {
            _positionalControls.Add(new List<IPositionalControl>());
            return this;
        }

        public IMessageKeyboardControlBuilder UseKeyboardButtonControl(string text, EventHandler<ControlHandlingEventArgs> handler, TimeSpan? limiterDelay = null, EventHandler<ControlHandlingEventArgs> onReleaseLimiterHandler = null)
        {
            var control = new KeyboardButton(text);
            control.HandleEvent += handler;
            _positionalControls.Last().Add(control);

            if (limiterDelay.HasValue)
            {
                (control as ILimitable).Limiter = new ConnectionLimiter(limiterDelay.Value, onReleaseLimiterHandler);
            }

            return this;
        }

        public IMessageBuilder SaveControls()
        {
            return this._telegramMessageBuilder.SaveControls(_positionalControls);
        }

        public IMessageKeyboardControlBuilder RequestPhoneNumber()
        {
            (_positionalControls.Last().Last() as KeyboardButton).RequestPhoneNumber = true;
            return this;
        }

        public IMessageKeyboardControlBuilder RequestLocation()
        {
            (_positionalControls.Last().Last() as KeyboardButton).RequestLocation = true;
            return this;
        }
    }
}

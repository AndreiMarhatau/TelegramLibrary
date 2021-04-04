using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Builders
{
    public class WindowBuilder : IWindowPropertiesSaver
    {
        private ITelegramServicePropertiesSaver _telegramServiceBuilder;
        private WindowBase _window;

        internal WindowBuilder(ITelegramServicePropertiesSaver telegramServiceBuilder, WindowBase window)
        {
            this._telegramServiceBuilder = telegramServiceBuilder;
            this._window = window;
        }

        IWindowBuilder IWindowPropertiesSaver.SaveMessage(Message message)
        {
            _window.Messages.Add(message);
            return this;
        }

        IWindowBuilder IWindowPropertiesSaver.SaveWindowControls(IEnumerable<WindowControlBase> controls)
        {
            _window.Controls = _window.Controls.Concat(controls);
            return this;
        }

        public IMessageBuilder UseMessage()
        {
            return new MessageBuilder(this);
        }

        public IWindowControlBuilder UseWindowControls()
        {
            return new WindowControlBuilder(this);
        }

        public ITelegramServiceBuilder SaveWindow()
        {
            return this._telegramServiceBuilder.SaveWindow(this._window);
        }
    }
}

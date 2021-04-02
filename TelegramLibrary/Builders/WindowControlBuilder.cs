using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;
using TelegramLibrary.Models.ArgsForEvents;
using TelegramLibrary.Models.MainControls;
using TelegramLibrary.Models.MessageControls;

namespace TelegramLibrary.Builders
{
    public class WindowControlBuilder : IWindowControlBuilder
    {
        private ITelegramServiceBuilder _telegramServiceBuilder;

        private List<MainControlBase> _controls = new List<MainControlBase>();

        internal WindowControlBuilder(ITelegramServiceBuilder telegramServiceBuilder)
        {
            this._telegramServiceBuilder = telegramServiceBuilder;
        }

        public IWindowControlBuilder UseTextInputControl(EventHandler<ControlHandlingEventArgs> handler)
        {
            if(_controls.Any(control => control.GetType() == typeof(TextInput)))
            {
                throw new InvalidOperationException("Every control in a window should be unique.");
            }
            var control = new TextInput();
            control.HandleEvent += handler;
            _controls.Add(control);
            return this;
        }

        public ITelegramServiceBuilder SaveControls()
        {
            return this._telegramServiceBuilder.SaveControls(_controls);
        }
    }
}

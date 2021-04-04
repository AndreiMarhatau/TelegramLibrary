using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;
using TelegramLibrary.Models.ArgsForEvents;
using TelegramLibrary.Models.WindowControls;
using TelegramLibrary.Models.WindowControls;

namespace TelegramLibrary.Builders
{
    public class WindowControlBuilder : IWindowControlBuilder
    {
        private IWindowPropertiesSaver _windowBuilder;

        private List<WindowControlBase> _controls = new List<WindowControlBase>();

        internal WindowControlBuilder(IWindowPropertiesSaver windowBuilder)
        {
            this._windowBuilder = windowBuilder;
        }

        public IWindowControlBuilder UseTextInputControl(EventHandler<ControlHandlingEventArgs> handler)
        {
            ValidateControls<TextInput>();
            var control = new TextInput();
            return this.AddControl(control, handler);
        }

        public IWindowBuilder SaveControls()
        {
            return this._windowBuilder.SaveWindowControls(_controls);
        }

        private void ValidateControls<T>() where T: WindowControlBase
        {
            if (_controls.Any(control => control.GetType() == typeof(T)))
            {
                throw new InvalidOperationException("Every control in a window should be unique.");
            }
        }

        private IWindowControlBuilder AddControl(WindowControlBase control, EventHandler<ControlHandlingEventArgs> handler)
        {
            control.HandleEvent += handler;
            _controls.Add(control);
            return this;
        }
    }
}

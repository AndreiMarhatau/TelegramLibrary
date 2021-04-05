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
    public class WindowControlBuilder : IWindowControlBuilder
    {
        private IWindowPropertiesSaver _windowBuilder;

        private List<WindowControlBase> _controls = new List<WindowControlBase>();

        internal WindowControlBuilder(IWindowPropertiesSaver windowBuilder)
        {
            this._windowBuilder = windowBuilder;
        }

        public IWindowControlBuilder UseTextInputControl(EventHandler<ControlHandlingEventArgs> handler, TimeSpan? limiterDelay = null, EventHandler<ControlHandlingEventArgs> onReleaseLimiterHandler = null)
        {
            AddControl<TextInput>(handler, limiterDelay, onReleaseLimiterHandler);
            return this;
        }

        public IWindowBuilder SaveControls()
        {
            return this._windowBuilder.SaveWindowControls(_controls);
        }

        public IWindowControlBuilder UsePhotoInputControl(EventHandler<ControlHandlingEventArgs> handler, TimeSpan? limiterDelay = null, EventHandler<ControlHandlingEventArgs> onReleaseLimiterHandler = null)
        {
            AddControl<PhotoInput>(handler, limiterDelay, onReleaseLimiterHandler);
            return this;
        }

        public IWindowControlBuilder UseVideoInputControl(EventHandler<ControlHandlingEventArgs> handler, TimeSpan? limiterDelay = null, EventHandler<ControlHandlingEventArgs> onReleaseLimiterHandler = null)
        {
            AddControl<VideoInput>(handler, limiterDelay, onReleaseLimiterHandler);
            return this;
        }

        private void AddControl<T>(EventHandler<ControlHandlingEventArgs> handler, TimeSpan? limiterDelay = null, EventHandler<ControlHandlingEventArgs> onReleaseLimiterHandler = null) where T: WindowControlBase, new()
        {
            var control = new T();

            if (_controls.Any(control => control.GetType() == control.GetType()))
            {
                throw new InvalidOperationException("Every control in a window should be unique.");
            }

            control.HandleEvent += handler;
            _controls.Add(control);

            if (limiterDelay.HasValue)
            {
                (control as ILimitable).Limiter = new ConnectionLimiter(limiterDelay.Value, onReleaseLimiterHandler);
            }
        }
    }
}

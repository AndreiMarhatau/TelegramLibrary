using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models.ArgsForEvents;

namespace TelegramLibrary.Builders
{
    public interface IWindowControlBuilder
    {
        IWindowControlBuilder UseTextInputControl(EventHandler<ControlHandlingEventArgs> handler, TimeSpan? limiterDelay = null, EventHandler<ControlHandlingEventArgs> onReleaseLimiterHandler = null);
        IWindowControlBuilder UsePhotoInputControl(EventHandler<ControlHandlingEventArgs> handler, TimeSpan? limiterDelay = null, EventHandler<ControlHandlingEventArgs> onReleaseLimiterHandler = null);
        IWindowControlBuilder UseVideoInputControl(EventHandler<ControlHandlingEventArgs> handler, TimeSpan? limiterDelay = null, EventHandler<ControlHandlingEventArgs> onReleaseLimiterHandler = null);
        IWindowBuilder SaveControls();
    }
}

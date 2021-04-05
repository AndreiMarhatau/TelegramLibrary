using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models.ArgsForEvents;

namespace TelegramLibrary.Builders
{
    public interface IMessageKeyboardControlBuilder
    {
        IMessageKeyboardControlBuilder CreateRow();
        IMessageKeyboardControlBuilder UseKeyboardButtonControl(string text, EventHandler<ControlHandlingEventArgs> handler, TimeSpan? limiterDelay = null, EventHandler<ControlHandlingEventArgs> onReleaseLimiterHandler = null);
        IMessageBuilder SaveControls();
    }
}

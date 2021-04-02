using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models.ArgsForEvents;

namespace TelegramLibrary.Builders
{
    public interface IMessageCallbackControlBuilder
    {
        IMessageCallbackControlBuilder CreateRow();
        IMessageCallbackControlBuilder UseCallbackButtonControl(string text, string data, EventHandler<ControlHandlingEventArgs> handler);
        ITelegramMessageBuilder SaveControls();
    }
}

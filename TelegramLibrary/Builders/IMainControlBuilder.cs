using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models.ArgsForEvents;

namespace TelegramLibrary.Builders
{
    public interface IMainControlBuilder
    {
        IMainControlBuilder UseCommandControl(string command, EventHandler<ControlHandlingEventArgs> handler, string description = null, TimeSpan? limiterDelay = null);
        ITelegramServiceBuilder SaveControls();
    }
}

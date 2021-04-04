using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Builders
{
    public interface IWindowBuilder
    {
        IMessageBuilder UseMessage(string text);
        IWindowBuilder SaveMessage(Message message);
        IWindowControlBuilder UseWindowControls();
        IWindowBuilder SaveWindowControls(IEnumerable<WindowControlBase> controls);
        ITelegramServiceBuilder SaveWindow();
    }
}

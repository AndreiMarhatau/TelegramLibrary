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
        IMessageBuilder UseMessage();
        IWindowControlBuilder UseWindowControls();
        ITelegramServiceBuilder SaveWindow();
    }

    public interface IWindowPropertiesSaver: IWindowBuilder
    {
        IWindowBuilder SaveMessage(Message message);
        IWindowBuilder SaveWindowControls(IEnumerable<WindowControlBase> controls);
    }
}

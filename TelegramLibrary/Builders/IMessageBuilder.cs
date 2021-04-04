using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Builders
{
    public interface IMessageBuilder
    {
        IMessageBuilder UseText(string text);
        IMessageBuilder SetIsGlobalHandlers(bool isGlobal);
        IWindowBuilder SaveMessage();
        IMessageKeyboardControlBuilder UseKeyboardControls();
        IMessageCallbackControlBuilder UseCallbackControls();
    }

    public interface IMessagePropertiesSaver: IMessageBuilder
    {
        IMessageBuilder SaveControls(IEnumerable<IEnumerable<IPositionalControl>> positionalControls);
    }
}

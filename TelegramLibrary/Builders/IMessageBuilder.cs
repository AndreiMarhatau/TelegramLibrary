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
        IWindowBuilder SaveMessage();
        IMessageKeyboardControlBuilder UseKeyboardControls();
        IMessageCallbackControlBuilder UseCallbackControls();
        IMessageBuilder SaveControls(IEnumerable<IEnumerable<IPositionalControl>> positionalControls);
    }
}

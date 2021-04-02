using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Builders
{
    public interface ITelegramMessageBuilder
    {
        ITelegramServiceBuilder SaveMessage();
        IMessageKeyboardControlBuilder UseKeyboardControls();
        IMessageCallbackControlBuilder UseCallbackControls();
        ITelegramMessageBuilder SaveControls(IEnumerable<IEnumerable<IPositionalControl>> positionalControls);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Models;

namespace TelegramLibrary.Internal
{
    internal class Storage
    {
        internal IEnumerable<MainControlBase> Controls { get; set; } = new List<MainControlBase>();
        internal IEnumerable<WindowBase> RegisteredWindows { get; set; } = new List<WindowBase>();

        internal MainControlBase FindHandlingControl(Update update)
        {
            return this.Controls.FirstOrDefault(control => control.IsAbleToProceed(update));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Models;
using TelegramLibrary.Models.ArgsForEvents;
using TelegramLibrary.Models.MainControls;

namespace TelegramLibrary.Internal
{
    internal class Storage
    {
        internal IEnumerable<MainControlBase> Controls { get; set; } = new List<MainControlBase>();
        internal DefaultControl DefaultControl { get; set; }
        internal IEnumerable<WindowBase> RegisteredWindows { get; set; } = new List<WindowBase>();

        internal MainControlBase FindHandlingControl(Update update)
        {
            return this.Controls.FirstOrDefault(control => control.IsAbleToProceed(update));
        }
    }
}

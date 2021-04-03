using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Models.WindowControls;

namespace TelegramLibrary.Models
{
    public abstract class WindowBase
    {
        internal IEnumerable<WindowControlBase> Controls { get; set; } = new List<WindowControlBase>();
        internal IList<Message> Messages { get; set; } = new List<Message>();

        public string GetFullName()
        {
            return this.GetType().FullName;
        }

        internal WindowControlBase FindHandlingControl(Update update)
        {
            return Controls.FirstOrDefault(control => control.IsAbleToProceed(update));
        }

        internal MessageControlBase FindMessageHandlingControl(Update update)
        {
            var positionalControls = Messages
                .SelectMany(msgs => msgs.PositionalControls)
                .SelectMany(controls => controls.Select(control => control as MessageControlBase));
            return positionalControls
                .FirstOrDefault(control => control.IsAbleToProceed(update));
        }
    }
}

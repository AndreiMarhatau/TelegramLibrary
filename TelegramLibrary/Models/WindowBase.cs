using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Models.ArgsForEvents;
using TelegramLibrary.Models.MainControls;

namespace TelegramLibrary.Models
{
    public sealed class WindowBase
    {
        private string _name;

        internal IEnumerable<WindowControlBase> Controls { get; set; } = new List<WindowControlBase>();
        public IList<Message> Messages { get; internal set; } = new List<Message>();
        internal DefaultControl DefaultControl { get; set; }

        public WindowBase(string name)
        {
            this._name = name;
        }

        public string GetFullName()
        {
            return _name;
        }

        internal WindowControlBase FindHandlingControl(Update update)
        {
            return Controls.FirstOrDefault(control => control.IsAbleToProceed(update));
        }

        internal MessageControlBase FindMessageHandlingControl(Update update, bool isGlobal = false)
        {
            var positionalControls = Messages
                .Where(message => message.IsGlobal == isGlobal)
                .SelectMany(msgs => msgs.PositionalControls)
                .SelectMany(controls => controls.Select(control => control as MessageControlBase));
            return positionalControls
                .FirstOrDefault(control => control.IsAbleToProceed(update));
        }
    }
}

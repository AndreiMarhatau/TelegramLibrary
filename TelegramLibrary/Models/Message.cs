using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Builders;
using TelegramLibrary.Editors;

namespace TelegramLibrary.Models
{
    public class Message
    {
        public bool IsGlobal { get; internal set; }
        internal IEnumerable<IEnumerable<IPositionalControl>> PositionalControls { get; set; } = new List<IEnumerable<IPositionalControl>>();
        public string Text { get; set; }
        public IMessageBuilder Edit()
        {
            return new MessageEditor(this);
        }
    }
}

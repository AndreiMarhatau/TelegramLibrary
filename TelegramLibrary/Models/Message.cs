using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramLibrary.Models
{
    public class Message
    {
        internal IEnumerable<IEnumerable<IPositionalControl>> PositionalControls { get; set; } = new List<IEnumerable<IPositionalControl>>();
        internal string Text { get; set; }
    }
}

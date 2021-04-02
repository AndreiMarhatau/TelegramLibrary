using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Internal
{
    internal class Storage
    {
        internal IEnumerable<WindowBase> RegisteredWindows { get; set; } = new List<WindowBase>();
    }
}

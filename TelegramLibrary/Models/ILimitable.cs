using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramLibrary.Models
{
    internal interface ILimitable
    {
        IConnectionLimiter Limiter { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramLibrary.Models
{
    public interface IConnectionLimiter
    {
        bool TryCapture(int id);
        void Release(int id);
    }
}

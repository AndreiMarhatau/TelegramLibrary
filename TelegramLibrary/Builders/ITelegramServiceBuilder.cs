using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Builders
{
    public interface ITelegramServiceBuilder
    {
        ITelegramServiceBuilder UseToken(string botToken);
        ITelegramServiceBuilder UseWebHookUrl(string url);
        ITelegramServiceBuilder UseDbConfiguration(Action<DbContextOptionsBuilder> action);
        IMainControlBuilder UseMainControls();
        ITelegramServiceBuilder SaveMainControls(IEnumerable<MainControlBase> controls);
        IWindowBuilder UseWindow(WindowBase window);
        ITelegramServiceBuilder SaveWindow(WindowBase window);
        ITelegramService GetService();
    }
}

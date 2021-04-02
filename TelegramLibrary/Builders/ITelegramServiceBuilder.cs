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
        ITelegramServiceBuilder UseStartWindow(WindowBase window);
        ITelegramServiceBuilder UseWindow(WindowBase window);
        ITelegramService GetService();
        ITelegramMessageBuilder UseMessage(Func<string> text);
        ITelegramServiceBuilder SaveMessage(Message message);
        IWindowControlBuilder UseControls();
        ITelegramServiceBuilder SaveControls(IEnumerable<MainControlBase> controls);
    }
}

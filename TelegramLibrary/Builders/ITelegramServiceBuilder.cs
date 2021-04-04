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
        ITelegramServiceBuilder RegisterCommands();
        IWindowBuilder UseWindow(WindowBase window);
        Task<ITelegramService> GetService();
    }

    public interface ITelegramServicePropertiesSaver: ITelegramServiceBuilder
    {
        ITelegramServiceBuilder SaveMainControls(IEnumerable<MainControlBase> controls);
        ITelegramServiceBuilder SaveWindow(WindowBase window);
    }
}

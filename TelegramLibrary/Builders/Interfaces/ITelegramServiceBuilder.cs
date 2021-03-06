using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;
using TelegramLibrary.Models.ArgsForEvents;
using TelegramLibrary.Repositories;

namespace TelegramLibrary.Builders
{
    public interface ITelegramServiceBuilder
    {
        ITelegramServiceBuilder UseToken(string botToken);
        ITelegramServiceBuilder UseWebHookUrl(string url);
        ITelegramServiceBuilder UseRepository(Func<IUserRepository> getRepository);
        IMainControlBuilder UseMainControls();
        ITelegramServiceBuilder RegisterCommands();
        ITelegramServiceBuilder UseDefaultHandler(EventHandler<ControlHandlingEventArgs> defaultHandler);
        IWindowBuilder UseWindow(string name);
        IConnectionLimiterBuilder UseConnectionLimiter();
        Task<ITelegramService> GetService();
    }

    public interface ITelegramServicePropertiesSaver: ITelegramServiceBuilder
    {
        ITelegramServiceBuilder SaveMainControls(IEnumerable<MainControlBase> controls);
        ITelegramServiceBuilder SaveWindow(WindowBase window);
        ITelegramServiceBuilder SaveLimiter(IConnectionLimiter limiter);
    }
}

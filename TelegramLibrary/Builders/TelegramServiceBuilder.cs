using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;
using TelegramLibrary.Repositories;

namespace TelegramLibrary.Builders
{
    public class TelegramServiceBuilder: ITelegramServiceBuilder
    {
        private string _token;
        private string _url;
        private Action<DbContextOptionsBuilder> _dbConfigurationAction;
        private List<WindowBase> _windows = new List<WindowBase>();
        private WindowBase _initialWindow;

        public TelegramServiceBuilder() { }

        public ITelegramServiceBuilder UseToken(string botToken)
        {
            this._token = botToken;
            return this;
        }

        public ITelegramServiceBuilder UseWebHookUrl(string url)
        {
            this._url = url;
            return this;
        }

        public ITelegramServiceBuilder UseDbConfiguration(Action<DbContextOptionsBuilder> action)
        {
            this._dbConfigurationAction = action;
            return this;
        }

        public ITelegramServiceBuilder UseStartWindow(WindowBase window)
        {
            this._initialWindow = window;
            return this.UseWindow(window);
        }

        public ITelegramServiceBuilder UseWindow(WindowBase window)
        {
            if (_windows.Select(window => window.GetFullName()).Any(windowName => windowName == window.GetFullName()))
            {
                throw new InvalidOperationException("Every window should be registered only once.");
            }
            this._windows.Add(window);
            return this;
        }

        public IWindowControlBuilder UseControls()
        {
            return new WindowControlBuilder(this);
        }

        ITelegramServiceBuilder ITelegramServiceBuilder.SaveControls(IEnumerable<MainControlBase> controls)
        {
            _windows.Last().Controls = _windows.Last().Controls.Concat(controls);
            return this;
        }

        public ITelegramMessageBuilder UseMessage(Func<string> text)
        {
            return new TelegramMessageBuilder(this, text());
        }

        ITelegramServiceBuilder ITelegramServiceBuilder.SaveMessage(Message message)
        {
            _windows.Last().Messages.Add(message);
            return this;
        }

        public ITelegramService GetService()
        {
            var repositoryFactory = new RepositoriesFactory(_dbConfigurationAction);
            var service = new TelegramService(_url, _token, repositoryFactory, _initialWindow);
            service.RegisterWindows(this._windows.Distinct().ToArray());
            return service;
        }
    }
}

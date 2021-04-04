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
        private bool _registerCommands = false;

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

        public IMainControlBuilder UseMainControls()
        {
            return new MainControlBuilder(this);
        }

        ITelegramServiceBuilder ITelegramServiceBuilder.SaveMainControls(IEnumerable<MainControlBase> controls)
        {
            LibraryStaticContext.Storage.Controls = LibraryStaticContext.Storage.Controls.Concat(controls);
            return this;
        }

        public ITelegramServiceBuilder RegisterCommands()
        {
            this._registerCommands = true;
            return this;
        }

        public IWindowBuilder UseWindow(WindowBase window)
        {
            if (_windows.Select(window => window.GetFullName()).Any(windowName => windowName == window.GetFullName()))
            {
                throw new InvalidOperationException("Every window should be registered only once.");
            }
            return new WindowBuilder(this, window);
        }

        ITelegramServiceBuilder ITelegramServiceBuilder.SaveWindow(WindowBase window)
        {
            _windows.Add(window);
            if(_initialWindow == null)
            {
                _initialWindow = window;
            }
            return this;
        }

        public async Task<ITelegramService> GetService()
        {
            this.UseMainControls()
                .UseCommandControl("start", (o, e) => e.TelegramInteractor.SendStartWindow())
            .SaveControls();
            var repositoryFactory = new RepositoriesFactory(_dbConfigurationAction);
            var service = new TelegramService(_url, _token, repositoryFactory, _initialWindow);
            service.RegisterWindows(this._windows.Distinct().ToArray());
            if (_registerCommands)
            {
                await service.RegisterCommands();
            }
            return service;
        }
    }
}

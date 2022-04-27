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
    public class TelegramServiceBuilder: ITelegramServicePropertiesSaver
    {
        private string _token;
        private string _url;
        private List<WindowBase> _windows = new List<WindowBase>();
        private WindowBase _initialWindow;
        private bool _registerCommands = false;
        private IConnectionLimiter _limiter;
        private Func<IUserRepository> _getRepository;

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

        public ITelegramServiceBuilder UseRepository(Func<IUserRepository> getRepository)
        {
            this._getRepository = getRepository;
            return this;
        }

        public IMainControlBuilder UseMainControls()
        {
            return new MainControlBuilder(this);
        }

        ITelegramServiceBuilder ITelegramServicePropertiesSaver.SaveMainControls(IEnumerable<MainControlBase> controls)
        {
            LibraryStaticContext.Storage.Controls = LibraryStaticContext.Storage.Controls.Concat(controls);
            return this;
        }

        public ITelegramServiceBuilder RegisterCommands()
        {
            this._registerCommands = true;
            return this;
        }

        public IWindowBuilder UseWindow(string name)
        {
            if (_windows.Select(window => window.GetFullName()).Any(windowName => windowName == name))
            {
                throw new InvalidOperationException("Every window should be registered only once.");
            }
            return new WindowBuilder(this, new WindowBase(name));
        }

        ITelegramServiceBuilder ITelegramServicePropertiesSaver.SaveWindow(WindowBase window)
        {
            _windows.Add(window);
            if(_initialWindow == null)
            {
                _initialWindow = window;
            }
            return this;
        }

        public IConnectionLimiterBuilder UseConnectionLimiter()
        {
            return new ConnectionLimiterBuilder(this);
        }

        public ITelegramServiceBuilder SaveLimiter(IConnectionLimiter limiter)
        {
            this._limiter = limiter;
            return this;
        }

        public ITelegramServiceBuilder UseDefaultHandler(EventHandler<ControlHandlingEventArgs> defaultHandler)
        {
            LibraryStaticContext.Storage.DefaultControl = new Models.MainControls.DefaultControl();
            LibraryStaticContext.Storage.DefaultControl.HandleEvent += defaultHandler;
            return this;
        }

        public async Task<ITelegramService> GetService()
        {
            if (_url == null)
            {
                throw new InvalidOperationException("Webhook url must be registered before this call.");
            }

            if (_token == null)
            {
                throw new InvalidOperationException("Bot token must be registered before this call.");
            }

            if (_initialWindow == null)
            {
                throw new InvalidOperationException("At least one window must be added before this call.");
            }

            if (_getRepository == null)
            {
                _getRepository = () => new PerRunUserRepository();
            }

            this.UseMainControls()
                .UseCommandControl("start", (o, e) => e.TelegramInteractor.SendStartWindow())
            .SaveControls();
            var service = new TelegramService(_url, _token, _getRepository, _initialWindow, _limiter);
            service.RegisterWindows(this._windows.Distinct().ToArray());
            if (_registerCommands)
            {
                await service.RegisterCommands();
            }
            return service;
        }
    }
}

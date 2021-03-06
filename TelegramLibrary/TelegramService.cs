using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramLibrary.Models;
using TelegramLibrary.Internal;
using TelegramLibrary.Repositories;
using TelegramLibrary.Extensions;
using TelegramLibrary.TelegramInteraction;
using TelegramLibrary.Models.MainControls;

namespace TelegramLibrary
{
    public class TelegramService: ITelegramService
    {
        private Storage _storage = LibraryStaticContext.Storage;
        private TelegramBotClient _telegramBotClient;
        private Func<IUserRepository> _getRepository;
        private WindowBase _initialWindow;
        private IConnectionLimiter _limiter;

        internal TelegramService(string webHookUrl, string botToken, Func<IUserRepository> getRepository, WindowBase initialWindow, IConnectionLimiter _limiter)
        {
            this._telegramBotClient = new TelegramBotClient(botToken);
            this._telegramBotClient.SetWebhookAsync(webHookUrl);
            this._getRepository = getRepository;
            this._initialWindow = initialWindow;
            this._limiter = _limiter;
        }

        internal void RegisterWindows(params WindowBase[] windows)
        {
            _storage.RegisteredWindows = _storage.RegisteredWindows.Concat(windows);
        }

        internal async Task RegisterCommands()
        {
            await this._telegramBotClient.SetMyCommandsAsync(
                _storage.Controls
                    .Where(control => control is Command && (control as Command).Description?.Length >= 3 && (control as Command).Description?.Length <= 256)
                    .Select(control => new BotCommand() { Command = (control as Command).CommandText, Description = (control as Command).Description }));
        }
        
        public async Task HandleUpdate(Update update)
        {
            if (_limiter?.TryCapture(update.GetFrom().Id) == false)
            {
                return;
            }

            ITelegramInteractor telegramInteractor = null;

            try
            {
                IUserRepository userRepository = _getRepository();
                UserModel user = await userRepository.GetOrCreateUserWithDefaultWindow(update.GetFrom().Id, _initialWindow.GetFullName());
                var window = _storage.RegisteredWindows.First(window => window.GetFullName().Equals(user.LastWindow));
                telegramInteractor = new TelegramInteractor(_telegramBotClient, update, user, _initialWindow, _storage, userRepository);

                var mainControl = _storage.FindHandlingControl(update);

                if (mainControl != null)
                {
                    mainControl.Handle(telegramInteractor);
                    return;
                }

                var messageControl = window.FindMessageHandlingControl(update);
                if (messageControl != null)
                {
                    messageControl.Handle(telegramInteractor);
                    return;
                }

                foreach (var _window in _storage.RegisteredWindows)
                {
                    var control = _window.FindMessageHandlingControl(update, true);
                    if (control != null)
                    {
                        control.Handle(telegramInteractor);
                        return;
                    }
                }

                var windowControl = window.FindHandlingControl(update);
                if (windowControl != null)
                {
                    windowControl.Handle(telegramInteractor);
                    return;
                }

                var defaultControl = window.DefaultControl ?? _storage.DefaultControl;

                if(defaultControl != null)
                {
                    defaultControl.Handle(telegramInteractor);
                    return;
                }
            }
            finally
            {
                _limiter?.Release(update.GetFrom().Id, telegramInteractor);
            }
        }
    }
}

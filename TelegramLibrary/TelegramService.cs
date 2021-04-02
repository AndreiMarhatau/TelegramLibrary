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
using TelegramLibrary.Repositories.UserRepo;

namespace TelegramLibrary
{
    public class TelegramService: ITelegramService
    {
        private Storage _storage = LibraryStaticContext.Storage;
        private TelegramBotClient _telegramBotClient;
        private RepositoriesFactory _repositoriesFactory;
        private WindowBase _initialWindow;

        internal TelegramService(string webHookUrl, string botToken, RepositoriesFactory repositoriesFactory, WindowBase initialWindow)
        {
            this._telegramBotClient = new TelegramBotClient(botToken);
            this._telegramBotClient.SetWebhookAsync(webHookUrl);
            this._repositoriesFactory = repositoriesFactory;
            this._initialWindow = initialWindow;
        }

        public void RegisterWindows(params WindowBase[] windows)
        {
            _storage.RegisteredWindows = _storage.RegisteredWindows.Concat(windows);
        }
        
        public async Task HandleUpdate(Update update)
        {
            IUserRepository userRepository = _repositoriesFactory.GetUserRepository();
            UserModel user = await userRepository.GetOrCreateUser(update.GetFrom().Id, _initialWindow.GetFullName());
            var window = user.WindowBase;

            var messageControl = window.FindMessageHandlingControl(update);
            var mainControl = window.FindHandlingControl(update);
            if (messageControl != null)
            {
                messageControl.Handle(new TelegramInteractor(_telegramBotClient, update, user, _initialWindow, _storage, userRepository));
            }
            else if(mainControl != null)
            {
                mainControl.Handle(new TelegramInteractor(_telegramBotClient, update, user, _initialWindow, _storage, userRepository));
            }
        }
    }
}

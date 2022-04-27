using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramLibrary.Extensions;
using TelegramLibrary.Internal;
using TelegramLibrary.Models;
using TelegramLibrary.Repositories;
using TelegramLibrary.TelegramInteraction.Converters;

namespace TelegramLibrary.TelegramInteraction
{
    public class TelegramInteractor: ITelegramInteractor
    {
        private TelegramBotClient _telegramBotClient;
        private Update _update;
        private UserModel _user;
        private WindowBase _initialWindow;
        private Storage _storage;
        private IUserRepository _userRepository;

        public Telegram.Bot.Types.Message Message => _update.GetMessage();
        public UserModel User => _user;

        public ITelegramBotClient TelegramBotClient => _telegramBotClient;

        internal TelegramInteractor(TelegramBotClient telegramBotClient, Update update, UserModel user, WindowBase initialWindow, Storage storage, IUserRepository userRepository)
        {
            this._telegramBotClient = telegramBotClient;
            this._update = update;
            this._user = user;
            this._initialWindow = initialWindow;
            this._storage = storage;
            this._userRepository = userRepository;
        }

        public async Task SendStartWindow()
        {
            await SendWindow(_initialWindow.GetFullName());
        }

        public async Task SendText(string text)
        {
            await this._telegramBotClient.SendTextMessageAsync(_update.GetMessage().Chat.Id, text);
        }

        public async Task SendWindow(string name)
        {
            var window = _storage.RegisteredWindows.Single(window => window.GetFullName() == name);

            if (!window.Messages.Any(m => m.PositionalControls.SelectMany(pc => pc).Any(c => c is Models.WindowControls.KeyboardButton)))
            {
                var msg = await _telegramBotClient.SendTextMessageAsync(_update.GetMessage().Chat.Id, ".", replyMarkup: new ReplyKeyboardRemove(), disableNotification: true);
                await _telegramBotClient.DeleteMessageAsync(_update.GetMessage().Chat.Id, msg.MessageId);
            }

            foreach (var message in window.Messages)
            {
                bool? isCallback = message.PositionalControls.Any() ? message.PositionalControls.First().First() is Models.WindowControls.CallbackButton : null;
                IReplyMarkup replyMarkup = null;
                if (isCallback == true)
                {
                    replyMarkup = message.PositionalControls.Any() ? new InlineKeyboardMarkup(message.PositionalControls
                        .Select(controlRow =>
                            controlRow
                            .Select(control =>
                                (control as Models.WindowControls.CallbackButton)
                                .ToTelegramControl()))) : null;
                }
                else if(isCallback == false)
                {
                    replyMarkup = message.PositionalControls.Any() ? new ReplyKeyboardMarkup(message.PositionalControls
                        .Select(controlRow =>
                            controlRow
                            .Select(control =>
                                (control as Models.WindowControls.KeyboardButton)
                                .ToTelegramControl())))
                    { ResizeKeyboard = true } : null;
                }
                await _telegramBotClient.SendTextMessageAsync(_update.GetMessage().Chat.Id, message.Text, replyMarkup: replyMarkup);
                await _userRepository.SetWindow(_user.Id, window.GetFullName());
            }
        }

        public async Task DeleteKeyboard(string text)
        {
            var replyMarkup = new ReplyKeyboardRemove();
            await _telegramBotClient.SendTextMessageAsync(_update.GetMessage().Chat.Id, text, replyMarkup: replyMarkup);
        }
    }
}

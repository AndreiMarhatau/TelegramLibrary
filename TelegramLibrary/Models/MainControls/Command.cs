using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TelegramLibrary.Extensions;

namespace TelegramLibrary.Models.MainControls
{
    internal class Command : MainControlBase, IPositionalControl
    {
        private string _command;

        internal string CommandText => _command;

        internal Command(string command)
        {
            if (!command.StartsWith('/'))
            {
                this._command = "/" + command.Trim();
            }
            else
            {
                this._command = command.Trim();
            }
        }

        internal override bool IsAbleToProceed(Update update)
        {
            return update.Type == Telegram.Bot.Types.Enums.UpdateType.Message && update.GetMessage().Text.Split().First().Equals(this._command, StringComparison.OrdinalIgnoreCase);
        }
    }
}

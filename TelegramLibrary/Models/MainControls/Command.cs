using Microsoft.EntityFrameworkCore;
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
        private string _description;

        internal string CommandText => _command;
        internal string Description => _description;

        internal Command(string command, string description)
        {
            if (!command.StartsWith('/'))
            {
                this._command = "/" + command.Trim();
            }
            else
            {
                this._command = command.Trim();
            }
            _description = description;
        }

        internal override bool IsAbleToProceed(Update update)
        {
            return update.GetMessage().Type == Telegram.Bot.Types.Enums.MessageType.Text && update.GetMessage().Text.Split().First().Equals(this._command, StringComparison.OrdinalIgnoreCase);
        }
    }
}

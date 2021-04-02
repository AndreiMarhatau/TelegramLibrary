using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;
using TelegramLibrary.Repositories.Models;

namespace TelegramLibrary.Repositories.Converters
{
    internal static class UserConverter
    {
        internal static UserModel ToUserModel(this User user)
        {
            return new UserModel()
            {
                Id = user.TelegramId,
                WindowBase = LibraryStaticContext.Storage.RegisteredWindows.First(window => window.GetFullName() == user.LastWindow),
            };
        }
    }
}

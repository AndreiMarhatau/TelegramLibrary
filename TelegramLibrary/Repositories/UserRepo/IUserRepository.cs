using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Repositories.UserRepo
{
    internal interface IUserRepository
    {
        Task<UserModel> GetOrCreateUser(int id, string initialWindow);
        Task SetWindow(int id, string windowName);
    }
}

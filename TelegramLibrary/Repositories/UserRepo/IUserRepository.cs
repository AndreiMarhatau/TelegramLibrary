using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<UserModel> GetOrCreateUser(long id, string initialWindow);
        Task SetWindow(long id, string windowName);
    }
}

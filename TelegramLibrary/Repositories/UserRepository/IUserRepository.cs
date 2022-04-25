using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetOrCreateUserWithDefaultWindow(long id, string initialWindow);
        Task SetWindow(long id, string windowName);
    }
}

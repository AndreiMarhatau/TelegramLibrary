using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramLibrary.Models;
using TelegramLibrary.Repositories.UserRepo;

namespace Sample.Models
{
    public class UserRepositoryMock: IUserRepository
    {
        public async Task<UserModel> GetOrCreateUser(long id, string initialWindow)
        {
            return new UserModel() { Id = id, LastWindow = initialWindow };
        }

        public async Task SetWindow(long id, string windowName)
        {

        }
    }
}

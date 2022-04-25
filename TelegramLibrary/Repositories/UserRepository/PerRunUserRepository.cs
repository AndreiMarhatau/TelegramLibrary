using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;

namespace TelegramLibrary.Repositories
{
    internal class PerRunUserRepository : IUserRepository
    {
        private static readonly IList<UserModel> _users = new List<UserModel>();

        public async Task<UserModel> GetOrCreateUserWithDefaultWindow(long id, string initialWindow)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if(user == null)
            {
                user = new UserModel() { Id = id, LastWindow = initialWindow };
                _users.Add(user);
            }

            return user;
        }

        public async Task SetWindow(long id, string windowName)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            if(user == null)
            {
                throw new InvalidOperationException("User doesn't exist.");
            }

            user.LastWindow = windowName;
        }
    }
}

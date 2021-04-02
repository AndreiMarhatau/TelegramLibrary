using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Models;
using TelegramLibrary.Repositories.Converters;
using TelegramLibrary.Repositories.Models;

namespace TelegramLibrary.Repositories.UserRepo
{
    internal class UserRepository : IUserRepository
    {
        private TelegramLibraryContext _context;

        public UserRepository(TelegramLibraryContext context)
        {
            this._context = context;
        }

        public async Task<UserModel> GetOrCreateUser(int id, string initialWindow)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.TelegramId == id);
            if(user == null)
            {
                user = new User() { TelegramId = id, LastWindow = initialWindow };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            return user.ToUserModel();
        }

        public async Task SetWindow(int id, string windowName)
        {
            (await _context.Users.SingleAsync(user => user.TelegramId == id)).LastWindow = windowName;
            await _context.SaveChangesAsync();
        }
    }
}

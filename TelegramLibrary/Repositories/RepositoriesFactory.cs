using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramLibrary.Repositories.UserRepo;

namespace TelegramLibrary.Repositories
{
    internal class RepositoriesFactory
    {
        private Action<DbContextOptionsBuilder> dbConfigurationAction;

        public RepositoriesFactory(Action<DbContextOptionsBuilder> dbConfigurationAction)
        {
            this.dbConfigurationAction = dbConfigurationAction;
        }

        internal IUserRepository GetUserRepository()
        {
            return new UserRepository(new Models.TelegramLibraryContext(dbConfigurationAction));
        }
    }
}

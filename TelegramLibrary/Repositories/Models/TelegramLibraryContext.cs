using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramLibrary.Repositories.Models
{
    internal class TelegramLibraryContext: DbContext
    {
        private Action<DbContextOptionsBuilder> _onConfiguring;

        internal DbSet<User> Users { get; set; }

        internal TelegramLibraryContext(Action<DbContextOptionsBuilder> onConfiguring): base()
        {
            this._onConfiguring = onConfiguring;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _onConfiguring(optionsBuilder);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramLibrary.Repositories.Models
{
    internal class User
    {
        public int Id { get; set; }
        public int TelegramId { get; set; }
        public string LastWindow { get; set; }
    }
}

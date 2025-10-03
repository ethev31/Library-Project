using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementBL
{
    public abstract class User
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public abstract void DisplayRole();
    }
}

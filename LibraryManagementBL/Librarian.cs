using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementBL
{
    public class Librarian : User
    {
        public override void DisplayRole()
        {
            Debug.WriteLine($"{Name} is a Librarian");
        }
    }
}

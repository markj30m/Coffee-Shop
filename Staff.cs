using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop
{
    class Staff : Person
    {
        
        public string Password { get; set; }
        private static int mStaffIndex = 0;
        public Staff(string name, string password) : base(name)
        {
            Password = password;
            personName = name;
            ID = mStaffIndex++ + ID;

        }
        

    }
}

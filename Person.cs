using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop
{
    public class Person
    {
        public string personName { get; set; }

        public string ID { get; set; }

        
        public Person(string name)
        {
            personName = name;
            ID = name.Substring(0,2);
        }
     
     }
}

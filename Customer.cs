    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop
{
   public class Customer : Person
    {
        public string MemberStatus { get; set; }
        public decimal Balance { get; set; }
        private static int mCustomerIndex = 0;
        public Customer(string name, decimal balance,string Status):base(name)
        {
          
            MemberStatus = Status;
            Balance = balance;       
            personName = name;
            ID =  mCustomerIndex++ + ID + MemberStatus.Substring(0, 1);
        }
      



    }
    

}

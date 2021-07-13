using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop
{
    class Order
    {
        public int OrderNumber {get; set;}
        public string CustomerName { get; set; }
        public string StaffName { get; set; }     
        public decimal ItemPrice { get; set; }
        public List<string> OrderItem { get; set; } = new List<string>();
        public string currentDate { get; set; } = DateTime.Now.ToString();
        public Order(int number, string customer, string staff, List<string>list, decimal price)
        {
            OrderNumber = number++;
            CustomerName = customer;
            StaffName = staff;
           
            ItemPrice = price;
            OrderItem = list;
        }
    }
}

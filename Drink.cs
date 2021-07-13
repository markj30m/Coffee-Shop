using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop
{
    public class Drink : Item
    {


        public Drink(string Name, decimal price, int stock) : base(Name, price, stock)
        {
            ItemName = Name;
            Price = price;
            StockLevel = stock;
        }
  
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop
{
     public class Item
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int StockLevel { get; set; }
        public Item(string Name, decimal price, int stock)
        {
            ItemName = Name;
            Price = price;
            StockLevel = stock;
        }
    }
}

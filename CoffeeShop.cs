using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop
{
    class CoffeeShop
    {
      
        private List<Customer> mCustomer = new List<Customer>();
        Customer customer1 = new Customer("Jane Buchanan", 45.00m,"Normal");
        Customer customer2 = new Customer("Dick Richard", 20.00m,"Bronze");
        Customer customer3 = new Customer("Frederick Johan", 10.00m,"Silver");
        Customer customer4 = new Customer("Anna Mclean", 10.00m,"Gold");

        private List<Staff> mStaff = new List<Staff>();
        Staff staff1 = new Staff("Stella Te", "12345");
        Staff staff2 = new Staff("Gene Bean", "hello");
        Staff staff3 = new Staff("Simon Grinder", "H3llo");


        private List<Drink> mDrink = new List<Drink>();
        Drink drink1 = new Drink("Cappuccino", 1.75m, 100);
        Drink drink2 = new Drink("Latte", 1.50m, 90);
        Drink drink3 = new Drink("Breakfast Tea", 0.75m, 125);
        Drink drink4 = new Drink("Green Tea", 0.75m, 50);

        private List<Food> mFood = new List<Food>();
        Food food1 = new Food("Muffin", 2.75m, 25);
        Food food2 = new Food("Cheese Toastie", 3.50m, 20);
        Food food3 = new Food("Croissant", 1.99m, 30);
        Food food4 = new Food("Brownie", 2.50m, 25);

        private List<Order> mOrder = new List<Order>();
        
        public CoffeeShop()
        {
            mCustomer.Add(customer1);
            mCustomer.Add(customer2);
            mCustomer.Add(customer3);
            mCustomer.Add(customer4);

            mStaff.Add(staff1);
            mStaff.Add(staff2);
            mStaff.Add(staff3);


            mDrink.Add(drink1);
            mDrink.Add(drink2);
            mDrink.Add(drink3);
            mDrink.Add(drink4);

            mFood.Add(food1);
            mFood.Add(food2);
            mFood.Add(food3);
            mFood.Add(food4);

        }

        public Boolean Login(string name, string password)
        {
            bool login = false;
            
            foreach(Staff success in mStaff)
            {
              
                if(success.personName == name && success.Password == password)
                {
                    login = true;
                    return login;
                 
                    
                }


            }
            
            return login; 
        }
        public List<Drink>ShowDrink()
        {
           
            List<Drink> drinkName = mDrink;


            return drinkName;

            
        }
        public List<Staff> StaffName()
        {

            List<Staff> StaffLogin = mStaff;


            return StaffLogin;


        }

        public List<Food> ShowFood()
        {

            List<Food> foodName = mFood;


            return foodName;


        }
        public List<Order> ShowOrder()
        {

            List<Order> orderName = mOrder;


            return orderName;


        }
        public List<Customer> ShowCustomer()
        {

            List<Customer> customerCombo = mCustomer;


            return customerCombo;

        }
        public List<Staff>ShowStaff()
        {
            List<Staff> StaffList = mStaff;

            return StaffList;
        }


        public void AddOrder(Order addOrder)
        {
                      
                mOrder.Add(addOrder);

        }

        public void AddFood(Food newFood,string name,decimal price,int Stock)
        {
            mFood.Add(newFood);
        }
        public void AddDrink(Drink newDrink, string name, decimal price, int Stock)
        {
            mDrink.Add(newDrink);
          
        }
        public void AddCustomer(Customer newCustomer)
        {
            mCustomer.Add(newCustomer);
        }
        public void AddEmployee(Staff NewStaff)
        {
            mStaff.Add(NewStaff);
        }
        public void RemoveCustomer(Customer Remove,string name)
        {
            
            Customer CustomerRemove = null;
            foreach (Customer remove in mCustomer)
            {
                if (remove.personName == name)
                {
                    CustomerRemove = remove;
                    break;
                }
            }     
            mCustomer.Remove(CustomerRemove);
        }

        public decimal MemberDiscount(string name, decimal spent)
        {
            decimal member = 0;
            foreach (Customer Discount in mCustomer)
            {
                if (Discount.personName == name)
                {
                    if (Discount.MemberStatus == "Bronze")
                    {
                        if (spent > 25 && spent <= 50)
                        {
                            member = spent *(2.50m/100);
                        }
                    }
                    else if (Discount.MemberStatus == "Silver")
                    {
                        if (spent > 50 && spent <= 125)
                        {
                            member = spent * (5m/100);
                        }
                    }
                    else if (Discount.MemberStatus == "Gold")
                    {
                        if (spent > 125)
                        {
                            member = spent * (7.5m/100);
                        }
                    }
                }
            }
           return member;
        }

        public decimal HappyHourDiscount(decimal price)
        {
            TimeSpan TimeToday = DateTime.Now.TimeOfDay;
            TimeSpan HappyHourStart = new TimeSpan(14, 0, 0);
            TimeSpan HappyHourEnd = new TimeSpan(15, 0, 0); 
            DayOfWeek DateNow = DateTime.Now.DayOfWeek;
            decimal discount =0 ;
            if(DateNow == DayOfWeek.Saturday || DateNow == DayOfWeek.Sunday )
            {

            }
            else
            {
                if(TimeToday > HappyHourStart && TimeToday < HappyHourEnd)
                {
               
                        discount = price * (10m/ 100);
                    
                }

            }
            return discount;
        }
        public decimal FoodHalfPrice(decimal price)
        {
            TimeSpan TimeToday = DateTime.Now.TimeOfDay;
            TimeSpan HalfPrice = new TimeSpan(16, 0, 0);
            decimal totalDiscount = 0;

            if (TimeToday >= HalfPrice)
            {

                totalDiscount = price * (50m/100);
 
            }

            return totalDiscount;
        }
        
    }
}

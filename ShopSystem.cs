using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Coffee_Shop
{
    public partial class ShopSystem : Form
    {
        DateTime date = DateTime.Now;
        private CoffeeShop mShop = new CoffeeShop();
        string items;
        decimal price;
        decimal totalAmount = 0;
        static int count = 0;   
        public ShopSystem()
        {
            InitializeComponent();
        }

        private void ShopUI_Load(object sender, EventArgs e)
        {         
            uiPurchaseTabPage.Enabled = false;
            uiStaffZoneTabPage.Enabled = false;
            uiStaffTabPage.Enabled = false;

          
        }

        private void uiLoginButton_Click(object sender, EventArgs e)
        {
            
            if (mShop.Login(uiUserTextBox.Text,uiPasswordTextBox.Text))
            {
                uiErrorLabel.Visible = true;
                uiErrorLabel.Text = "Login Successful";
                uiErrorLabel.BackColor = Color.Green;

                uiPurchaseTabPage.Enabled = true;
                uiStaffZoneTabPage.Enabled = true;
                uiStaffTabPage.Enabled = true;


                ItemList();
                CustomerList();
                FoodList();
                DrinkList();

                uiItemTypeComboBox.SelectedIndex = 0;
                uiMemberComboBox.SelectedIndex = 0;
            }
            else
            {
                uiErrorLabel.Visible = true;
                uiErrorLabel.Text = "Error wrong name or password";
                uiErrorLabel.BackColor = Color.Red;
            }
        }

        private void uiLogOutButton_Click(object sender, EventArgs e)
        {
            uiPurchaseTabPage.Enabled = false;
            uiStaffZoneTabPage.Enabled = false;
            uiStaffTabPage.Enabled = false;
            uiErrorLabel.Visible = false;
            uiUserTextBox.Text = string.Empty;
            uiPasswordTextBox.Text = string.Empty;
            uiItemListView.Items.Clear();
            uiConfirmLabel.Visible = false;
        }     

        private void uiItemsAddButton_Click(object sender, EventArgs e)
        {
        string staffName = uiUserTextBox.Text;
        string customerName = uiCustomerComboBox.Text;
        string items;
        decimal price;   
        
        int amount = 0;
 


            foreach (Drink stock in mShop.ShowDrink())
            {
                if (uiItemListView.SelectedItems[0].SubItems[0].Text == stock.ItemName)
                {
                    if (stock.StockLevel > 0)
                    {                                             
                            amount++;
                            ListViewItem drink = new ListViewItem(uiItemListView.SelectedItems[0].SubItems[0].Text);
                            drink.SubItems.Add(Convert.ToString(amount));
                            price = decimal.Parse(uiItemListView.SelectedItems[0].SubItems[1].Text);
                            decimal happyhour = mShop.HappyHourDiscount(price);
                            price -= Math.Round(happyhour,2);
                            drink.SubItems.Add(Convert.ToString(price));
                            uiOrderListView.Items.Add(drink);                  
                    }
                }
            }
                
                    foreach(Food foodStock in mShop.ShowFood())
                    {
                        if(uiItemListView.SelectedItems[0].SubItems[0].Text == foodStock.ItemName)
                        {
                            if(foodStock.StockLevel > 0)
                            {                                                              
                                    amount++;
                                    ListViewItem drink = new ListViewItem(uiItemListView.SelectedItems[0].SubItems[0].Text);
                                    drink.SubItems.Add(Convert.ToString(amount));
                                    price = decimal.Parse(uiItemListView.SelectedItems[0].SubItems[1].Text);
                                   decimal halfPrice = mShop.FoodHalfPrice(price);
                                   price -= Math.Round(halfPrice,2);
                                    drink.SubItems.Add(Convert.ToString(price));
                                    uiOrderListView.Items.Add(drink);                                                                                                
                            }                       
                        }
                    }
           

            for (int i = 0; i < uiOrderListView.Items.Count; i++)          // adds all the prices in the order list into a total price 
            {
                totalAmount += Convert.ToDecimal(uiOrderListView.Items[i].SubItems[2].Text);
                break;
            }
          //  uiTotalTextBox.Text = Convert.ToString(totalAmount);

          decimal pricing = mShop.MemberDiscount(customerName, totalAmount);
            totalAmount -= Math.Round(pricing,2);
            uiTotalTextBox.Text = Convert.ToString(totalAmount);
        

        }

        private void uiPurchaseButton_Click(object sender, EventArgs e)
        {
            string staffName = uiUserTextBox.Text;
            string customerName = uiCustomerComboBox.Text;
            
            List<string> ItemOrder = new List<string>();
            uiHistoryTextBox.Text = String.Empty;

            foreach (Customer purchase in mShop.ShowCustomer())
            {
                if (uiCustomerComboBox.Text == purchase.personName)   
                {
                    if (decimal.Parse(uiTotalTextBox.Text) < purchase.Balance)
                    {
                        foreach (ListViewItem i in uiOrderListView.Items)       // converts and save items in the uiOrderListView to the order class will loop until the loop number is higher than the ListView Count
                        {
                           
                            price = totalAmount;
                            items = i.SubItems[0].Text;
                            ItemOrder.Add(items);                    
                                                       
                        }
                        mShop.AddOrder(new Order(count, customerName, staffName, ItemOrder, price));
                        count++;


                        purchase.Balance = purchase.Balance - Convert.ToDecimal(uiTotalTextBox.Text);

                        uiConfirmLabel.Visible = true;
                        uiConfirmLabel.Text = "Purchase Successful";
                        uiConfirmLabel.BackColor = Color.Green;



                        UpdateHistory();
                        DrinkStock();
                        FoodStock();  
                    }
                    break;
                }
                else
                {
                    uiConfirmLabel.Visible = true;
                    uiConfirmLabel.Text = "Purchase Fail";
                    uiConfirmLabel.BackColor = Color.Red;
                }
            }   
        }

        private void uiSaveButton_Click(object sender, EventArgs e)
        {

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+@"\Data.txt";

            File.WriteAllText(path, uiHistoryTextBox.Text);


        }

        private void uiUpdateButton_Click(object sender, EventArgs e)
        {
            uiItemListView.Items.Clear();
            ItemList();
        }

        private void uiClearButton_Click(object sender, EventArgs e)
        {
            uiOrderListView.Items.Clear();
            totalAmount = 0;
            uiTotalTextBox.Text = String.Empty;
        }

        private void uiAddItemButton_Click(object sender, EventArgs e)
        {
           

            if (String.IsNullOrWhiteSpace(uiItemNameTextBox.Text) || String.IsNullOrWhiteSpace(Convert.ToString(uiItemPriceTextBox.Text)))
            {

            }
            else if (String.IsNullOrWhiteSpace(Convert.ToString(uiStocklTextBox.Text)))
            {
                string name = uiItemNameTextBox.Text;
                decimal price = Convert.ToDecimal(uiItemPriceTextBox.Text);
                int stock = 50;

                if (uiItemTypeComboBox.Text == "Food")
                {
                    mShop.AddFood(new Food(name, price, stock), name, price, stock);
                }
                else
                {
                    mShop.AddDrink(new Drink(name, price, stock), name, price, stock);
                }
            }
            else 
            {
                string name = uiItemNameTextBox.Text;
                decimal price = Convert.ToDecimal(uiItemPriceTextBox.Text);
                int stock = Convert.ToInt16(uiStocklTextBox.Text);

                if (uiItemTypeComboBox.Text == "Food")
                {
                    mShop.AddFood(new Food(name, price, stock), name, price, stock);
                }
                else
                {
                    mShop.AddDrink(new Drink(name, price, stock), name, price, stock);
                }
            }

        }

        private void uiAddCustomerButton_Click(object sender, EventArgs e)
        {
            uiCustomerComboBox.Text = String.Empty;
           
            string status = uiMemberComboBox.Text;
            
            if(String.IsNullOrWhiteSpace(uiCustomerFirstTextBox.Text) || String.IsNullOrWhiteSpace(Convert.ToString(uiBalanceTextBox.Text)))
            {

            }
            else
            {
                string name = uiCustomerFirstTextBox.Text;
                decimal balance = Convert.ToDecimal(uiBalanceTextBox.Text);

                 if (balance < 10m)
                {
                    uiBalanceWarningLabel.Text = "Balance must be over £10";
                    uiBalanceWarningLabel.BackColor = Color.Red;
                    uiBalanceWarningLabel.Visible = true;
                }
                 else
                {
                    mShop.AddCustomer(new Customer(name,balance,status));
                }
               
            }


            CustomerList();

            
        }

        private void uiCustomerListButton_Click(object sender, EventArgs e)
        {
            uiCustomerInformationTextbox.Text = String.Empty;

            List<Customer> ShowCustomer = mShop.ShowCustomer();
            foreach (Customer CustomerList in ShowCustomer)
            {
                
                uiCustomerInformationTextbox.AppendText(CustomerList.ID +" | " +CustomerList.personName +" | " + CustomerList.Balance+" | " +CustomerList.MemberStatus +"\r\n\r\n");

                

            }
        }

        private void uiRemoveCustomerButton_Click(object sender, EventArgs e)
        {

            string name = uiRemoveFirstNameTextBox.Text;

            string id = uiRemoveIdTextBox.Text;
          
            string status = uiMemberComboBox.Text;


            Customer CustomerRemove = null;
            if (String.IsNullOrWhiteSpace(uiBalanceTextBox.Text))
            {

            }
            else
            {

             decimal balance = Convert.ToDecimal(uiBalanceTextBox.Text);

              foreach (Customer remove in mShop.ShowCustomer())
            {
                if (remove.personName == name && remove.ID == id)
                {
                    CustomerRemove = remove;
                    break;
                }
            }
            mShop.ShowCustomer().Remove(CustomerRemove);
            }
            CustomerList();

        }

        private void uiUpdateItemButton_Click(object sender, EventArgs e)
        {
 

                foreach(Food UpdateFood in mShop.ShowFood())
                {
                if (String.IsNullOrWhiteSpace(Convert.ToString(uiUpdateFoodStockTextBox.Text)))
                {

                }
                else if(uiUpdateFoodComboBox.Text == UpdateFood.ItemName)
                    {
                    int updateStock = Convert.ToInt16(uiUpdateFoodStockTextBox.Text);
                    UpdateFood.StockLevel += updateStock;
                    }
                }
       

        }
       

        private void uiUpdateDrinkButton_Click(object sender, EventArgs e)
        {
           

            foreach (Drink UpdateDrink in mShop.ShowDrink())
            {
                if (String.IsNullOrWhiteSpace(Convert.ToString(uiUpDateDrinkStockTextBox.Text)))
                {

                }
                else if (uiDrinkUpDateCombobox.Text == UpdateDrink.ItemName)
                {
                    int updateStock = Convert.ToInt16(uiUpDateDrinkStockTextBox.Text);
                    UpdateDrink.StockLevel += updateStock;
                }
            }


        }
        private void uiAddEmployeeButton_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(uiStaffNameTextBox.Text) || String.IsNullOrWhiteSpace(Convert.ToString(uiStaffPasswordTextBox.Text)))
            {

            }
            else
            {
                string name = uiStaffNameTextBox.Text;
                string password = uiStaffPasswordTextBox.Text;

                  
               
                    mShop.AddEmployee(new Staff(name,password));
                

            }
        }
        public void FoodList()
        {
            uiUpdateFoodComboBox.DataSource = null;
            uiUpdateFoodComboBox.DisplayMember = "ItemName";
            uiUpdateFoodComboBox.ValueMember = "ItemName";
            uiUpdateFoodComboBox.DataSource = mShop.ShowFood();
            uiUpdateFoodComboBox.SelectedIndex = 0;
        }
        public void DrinkList()
        {
            uiDrinkUpDateCombobox.DataSource = null;
            uiDrinkUpDateCombobox.DisplayMember = "ItemName";
            uiDrinkUpDateCombobox.ValueMember = "ItemName";
            uiDrinkUpDateCombobox.DataSource = mShop.ShowDrink();
            uiDrinkUpDateCombobox.SelectedIndex = 0;
        }
        public void CustomerList()
        {

            uiCustomerComboBox.DataSource = null;
            uiCustomerComboBox.DisplayMember = "personName";
            uiCustomerComboBox.ValueMember = "personName";
            uiCustomerComboBox.DataSource = mShop.ShowCustomer();
            uiCustomerComboBox.SelectedIndex = 0;
        }
        public void ItemList()
        {

            List<Drink> showDrink = mShop.ShowDrink();
                foreach(Drink drinkList in mShop.ShowDrink())
                {
                ListViewItem drink = new ListViewItem(drinkList.ItemName);
                drink.SubItems.Add(Convert.ToString(drinkList.Price));
                drink.SubItems.Add(Convert.ToString(drinkList.StockLevel));
                uiItemListView.Items.Add(drink);

            }

            List<Food> showFood = mShop.ShowFood();
                foreach (Food foodList in mShop.ShowFood())
                {
                ListViewItem food = new ListViewItem(foodList.ItemName);
                food.SubItems.Add(Convert.ToString(foodList.Price));
                food.SubItems.Add(Convert.ToString(foodList.StockLevel));
                uiItemListView.Items.Add(food);

            }
        }

        private void uiStaffButton_Click(object sender, EventArgs e)
        {
            uiStaffInformation.Text = String.Empty;

            List<Staff> ShowStaff = mShop.ShowStaff();
            foreach (Staff StaffInfo in ShowStaff)
            {

                uiStaffInformation.AppendText(StaffInfo.ID + " | " + StaffInfo.personName + " | " + StaffInfo.Password + "\r\n\r\n");



            }
            
        }
        public void DrinkStock()
        {
            foreach (Drink drink in mShop.ShowDrink())                       
            {

                for (int i = 0; i < uiOrderListView.Items.Count; i++)
                {
                    if (uiOrderListView.Items[i].SubItems[0].Text == drink.ItemName)
                    {
                        drink.StockLevel = drink.StockLevel - 1;


                    }

                }
            }
        }
        public void FoodStock()
        {
            foreach (Food food in mShop.ShowFood())
            {
                for (int i = 0; i < uiOrderListView.Items.Count; i++)
                {
                    if (uiOrderListView.Items[i].SubItems[0].Text == food.ItemName)
                    {
                        food.StockLevel = food.StockLevel - 1;
                    }
                }
            }
        }
        public void UpdateHistory()
        {
            List<Order> showHistory = mShop.ShowOrder();


            foreach (Order history in showHistory)
            {

                uiHistoryTextBox.AppendText(history.OrderNumber + " | " + history.CustomerName + " | " + history.StaffName + " | " + history.ItemPrice + " | " + history.currentDate + " | " + "\r\n\r\n");
                foreach (String showItems in history.OrderItem)
                {
                    uiHistoryTextBox.AppendText(showItems + "\r\n\r\n");

                }

            }
        }

    }
}

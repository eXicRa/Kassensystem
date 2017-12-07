using CustomComponents;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DataAccess
{

    /// <summary>
    /// Class shopping card of checkout system
    /// Eric, René
    /// </summary>
    public class ShoppingCart
    {

        public List<ShoppingCartItem> shoppingCartItems;
        private TextBox tbTotalAmount;
        public int MwSt { get; set; }

        //Create method of shopping cart, creates a new shopping card list of type OrderPositions
        public ShoppingCart(TextBox tb)
        {
            shoppingCartItems = new List<ShoppingCartItem>();
            tbTotalAmount = tb;
            MwSt = 19; //default 19%
        }
        //Method cleans the shopping card list, so all objects will be deleted
        //Eric
        public void Free()
        {
            if (shoppingCartItems != null)
            {
                for (int i = shoppingCartItems.Count - 1; i >= 0; i--)
                {
                    DeleteItem(shoppingCartItems[i]);
                }
            }
        }

        //René
        public decimal CalculateTotalAmount()
        {
            decimal totalAmount = 0;
            foreach (var item in shoppingCartItems)
            {
                var tempPrice = item.Orderposition.Amount * item.Orderposition.Product.Price;
                totalAmount += tempPrice;
            }

            totalAmount = (totalAmount * MwSt / 100) + totalAmount;
            tbTotalAmount.Text = totalAmount.ToString("c2");
            return totalAmount;
        }

        //Method adds a new product object to the shopping card list, default value of amount is 1
        //Eric 
        public ShoppingCartItem AddProduct(Product product, out bool isNewProduct, int amount = 1)
        {
            isNewProduct = true;
            if (product != null)
            {
                foreach (var item in shoppingCartItems)
                {
                    if (item.Orderposition.Product.Id == product.Id)
                    {
                        Alter(item, item.Orderposition.Amount + amount);
                        isNewProduct = false;
                        CalculateTotalAmount();
                        return item;
                    }
                }
                Orderposition orderPos = new Orderposition();

                orderPos.Product = product;
                orderPos.Amount = amount;

                ShoppingCartItem shoppingCartItem = new ShoppingCartItem();
                shoppingCartItem.Orderposition = orderPos;

                shoppingCartItems.Add(shoppingCartItem);

                CalculateTotalAmount();
                return shoppingCartItem;
            }
            return null;
        }
        //Method alter the amount of the given product object from the shopping card list
        //Eric 
        public void Alter(ShoppingCartItem shoppingCartItem, int newAmount)
        {
            if (shoppingCartItems != null)
            {
                if (newAmount <= 0)
                {
                    DeleteItem(shoppingCartItem);
                    return;
                }
                shoppingCartItem.Orderposition.Amount = newAmount;

                CalculateTotalAmount();

                shoppingCartItem.TextBox.Text = newAmount.ToString();
                decimal price = shoppingCartItem.Orderposition.Amount * shoppingCartItem.Orderposition.Product.Price;
                shoppingCartItem.Label.Text = price.ToString("c2") + $" {shoppingCartItem.Orderposition.Product.Description}";
            }
        }

        //Method deletes the given product object from the shopping card list
        //Eric
        public void DeleteItem(ShoppingCartItem item)
        {
            if (shoppingCartItems != null)
            {
                foreach (var item_ in shoppingCartItems)
                {
                    if (item_ == item)
                    {
                        item_.Panel.Parent.Controls.Remove(item_.Panel);
                        shoppingCartItems.Remove(item_);
                        CalculateTotalAmount();
                        break;
                    }
                }
            }
        }

        //René
        public void Alter(object sender, EventArgs e)
        {
            var button = sender as CustomButton;
            if (button != null)
            {
                var shoppingCartItem = button.Obj as ShoppingCartItem;
                if (shoppingCartItem != null)
                {
                    int amount;
                    if (button.Text == "-")
                    {
                        amount = shoppingCartItem.Orderposition.Amount - 1;
                    }
                    else
                    {
                        amount = shoppingCartItem.Orderposition.Amount + 1;
                    }

                    Alter(shoppingCartItem, amount);
                }
            }
        }

        //Eric
        //Method save the order and oderpositions to the database
        public void SaveToDatabase()
        {
            if (this.shoppingCartItems != null)
            {

                Order order = new Order();
                order.Date = DateTime.Now;
                Employee employee = new Employee();
                employee.Id = 1;
                order.Employee = employee;

                //Insert order to database table bestellung
                string dateStr = order.Date.ToString("yyyy-MM-dd hh:mm:ss");
                string sql = $"INSERT INTO bestellung (Datum,FK_Mitarbeiter_ID) VALUES ('{dateStr}'," +
                        $"{order.Employee.Id.ToString()})";
                Database.ExcecuteCommand(sql);

                //Get last order id from database table bestellung
                sql = "SELECT max(id) as last_item FROM bestellung";

                var myReader = Database.ExcecuteCommand(sql);

                while (myReader.Read())
                {
                    int tempID;
                    int.TryParse(myReader["last_item"].ToString(), out tempID);
                    //var anotherTempID = myReader["last_item"] as int?;
                    order.Id = tempID;
                }

                //Insert all orderpositions to database table bestellposition with order id
                foreach (ShoppingCartItem item in shoppingCartItems)
                {

                    sql = $"INSERT INTO bestellposition (Menge,FK_Produkt_ID," +
                        $"FK_Bestellung_ID) VALUES( {item.Orderposition.Amount},{item.Orderposition.Product.Id}" +
                        $",{order.Id})";

                    Database.ExcecuteCommand(sql);


                }

            }

        }


    }
}

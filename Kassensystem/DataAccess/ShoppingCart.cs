using CustomComponents;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DataAccess
{
    //Class shopping card of checkout system
    public class ShoppingCart
    {

        public List<ShoppingCartItem> shoppingCartItems;
        private TextBox tbTotalAmount;

        //Create method of shopping card, creates a new shopping card list of type OrderPositions
        public ShoppingCart(TextBox tb)
        {
            shoppingCartItems = new List<ShoppingCartItem>();
            tbTotalAmount = tb;
        }
        //Method cleans the shopping card list, so all objects will be deleted
        public void Free()
        {
            if (shoppingCartItems != null)
            {
                shoppingCartItems.Clear();
            }
        }

        public void CalculateTotalAmount()
        {
            double totalAmount = 0;
            foreach (var item in shoppingCartItems)
            {
                var tempPrice = item.orderposition.Amount * item.orderposition.Product.Price;
                totalAmount += tempPrice;
            }

            tbTotalAmount.Text = totalAmount.ToString("c2");
        }

        //Method adds a new product object to the shopping card list, default value of amount is 1
        public ShoppingCartItem AddProduct(Product product, out bool isNewProduct, int amount = 1)
        {
            isNewProduct = true;
            if (product != null)
            {
                foreach (var item in shoppingCartItems)
                {
                    if (item.orderposition.Product.Id == product.Id)
                    {
                        Alter(item, item.orderposition.Amount + amount);
                        isNewProduct = false;
                        CalculateTotalAmount();
                        return item;
                    }
                }
                Orderposition orderPos = new Orderposition();

                orderPos.Product = product;
                orderPos.Amount = amount;

                ShoppingCartItem shoppingCartItem = new ShoppingCartItem();
                shoppingCartItem.orderposition = orderPos;

                shoppingCartItems.Add(shoppingCartItem);

                CalculateTotalAmount();
                return shoppingCartItem;
            }
            return null;
        }
        //Method alter the amount of the given product object from the shopping card list
        public void Alter(ShoppingCartItem shoppingCartItem, int newAmount)
        {
            if (shoppingCartItems != null)
            {
                if (newAmount <= 0)
                {
                    DeleteProduct(shoppingCartItem);
                    return;
                }
                shoppingCartItem.orderposition.Amount = newAmount;

                CalculateTotalAmount();

                shoppingCartItem.textBox.Text = newAmount.ToString();
                var price = shoppingCartItem.orderposition.Amount * shoppingCartItem.orderposition.Product.Price;
                shoppingCartItem.label.Text = price.ToString("c2") + $" {shoppingCartItem.orderposition.Product.Description}";
            }
        }

        //Method deletes the given product object from the shopping card list
        public void DeleteProduct(ShoppingCartItem item)
        {
            if (shoppingCartItems != null)
            {
                foreach (var item_ in shoppingCartItems)
                {
                    if (item_ == item)
                    {
                        item_.panel.Parent.Controls.Remove(item_.panel);
                        shoppingCartItems.Remove(item_);
                        CalculateTotalAmount();
                        break;
                    }
                }
            }
        }

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
                        amount = shoppingCartItem.orderposition.Amount - 1;
                    }
                    else
                    {
                        amount = shoppingCartItem.orderposition.Amount + 1;
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

                    var  myReader = Database.ExcecuteCommand(sql);

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
                        $"FK_Bestellung_ID) VALUES( {item.orderposition.Amount},{item.orderposition.Product.Id}" +
                        $",{order.Id})";

                    Database.ExcecuteCommand(sql);
                        

                }

            }

        }


    }
}

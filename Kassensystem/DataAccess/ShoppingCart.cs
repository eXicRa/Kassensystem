using CustomComponents;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    //Class shopping card of checkout system
    public class ShoppingCart
    {

        public List<ShoppingCartItem> shoppingCartItems;

        //Create method of shopping card, creates a new shopping card list of type OrderPositions
        public ShoppingCart()
        {
            shoppingCartItems = new List<ShoppingCartItem>();

        }
        //Method cleans the shopping card list, so all objects will be deleted
        public void Free()
        {
            if (shoppingCartItems != null)
            {
                shoppingCartItems.Clear();
            }
        }
        //Method adds a new product object to the shopping card list, default value of amount is 1
        public ShoppingCartItem AddProduct(Product product, out bool isNewProduct, int amount = 1)
        {
            isNewProduct = true;
            if (product != null)
            {
                foreach (var item in shoppingCartItems)
                {
                    if (item.orderposition.Product == product)
                    {
                        Alter(item, item.orderposition.Amount + amount);
                        isNewProduct = false;
                        return item;
                    }
                }
                Orderposition orderPos = new Orderposition();

                orderPos.Product = product;
                orderPos.Amount = amount;

                ShoppingCartItem shoppingCartItem = new ShoppingCartItem();
                shoppingCartItem.orderposition = orderPos;

                shoppingCartItems.Add(shoppingCartItem);

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

                shoppingCartItem.textBox.Text = newAmount.ToString();
                var price = shoppingCartItem.orderposition.Amount * shoppingCartItem.orderposition.Product.Price;
                shoppingCartItem.label.Text = $"{price}€ {shoppingCartItem.orderposition.Product.Description}";
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


    }
}

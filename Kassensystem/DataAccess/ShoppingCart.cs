using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess 
{
    //Class shopping card of checkout system
    class ShoppingCart
    {

        public List<Orderposition> OrderPositions;

        //Create method of shopping card, creates a new shopping card list of type OrderPositions
        public ShoppingCart()
        {
            OrderPositions = new List<Orderposition>();

        }
        //Method cleans the shopping card list, so all objects will be deleted
        public void FreeCard()
        {
            if (OrderPositions != null)
            {
                OrderPositions.Clear();
            }
        }
        //Method adds a new product object to the shopping card list, default value of amount is 1
        public void AddProduct(Product item, int amount = 1)
        {
            if (item != null)
            {

                Orderposition orderPos = new Orderposition();

                orderPos.Product = item;
                orderPos.Amount = amount;

                OrderPositions.Add(orderPos);
            }
        }
        //Method alter the amount of the given product object from the shopping card list
        public void AlterShoppingCard(Product item, int newAmount)
        {
            if (OrderPositions != null)
            {

                foreach (Orderposition orderPos_ in OrderPositions)
                {
                    if (orderPos_.Product == item)
                    {
                        //Amount <= 0, so delete the order pos with product
                        if (newAmount <= 0)
                        {
                            DeleteProductFromShoppingCard(item);
                        }
                        else //Amount > 0, so the amount changed + or -
                        {
                            orderPos_.Amount = newAmount;
                        }
                    }
                }

            }
        }

        //Method deletes the given product object from the shopping card list
        public void DeleteProductFromShoppingCard(Product item)
        {
            if (OrderPositions != null)
            {
                foreach (Orderposition orderPos_ in OrderPositions)
                {
                    if (orderPos_.Product == item)
                    {
                        OrderPositions.Remove(orderPos_);
                    }
                }
            }
        }
    }
}

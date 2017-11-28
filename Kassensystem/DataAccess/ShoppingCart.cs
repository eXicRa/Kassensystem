using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess //Yolo very nice shopping cart
{
    class ShoppingCart
    {
        public List<Orderposition> OrderPositions;

        public ShoppingCart()
        {
            OrderPositions = new List<Orderposition>();

        }

        public void FreeCard()
        {
            if (OrderPositions != null)
            {
                OrderPositions.Clear();
            }
        }

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
    }
}

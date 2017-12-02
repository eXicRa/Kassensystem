using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomComponents;
using System.Windows.Forms;

namespace DataAccess
{
    public class ShoppingCartItem
    {
        public Orderposition orderposition { get; set; }
        public CustomPanel panel { get; set; }
        public CustomButton buttonMinus { get; set; }
        public CustomButton buttonPlus { get; set; }
        public TextBox textBox { get; set; }
        public Label label { get; set; }

    }
}

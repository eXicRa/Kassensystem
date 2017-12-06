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
        public Orderposition Orderposition { get; set; }
        public CustomPanel Panel { get; set; }
        public CustomButton ButtonMinus { get; set; }
        public CustomButton ButtonPlus { get; set; }
        public TextBox TextBox { get; set; }
        public Label Label { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kassensystem
{
    public partial class FormCheckout : Form
    {
        private double _price;
        public bool CheckOutSuccess { get; set; }

        public FormCheckout(double price)
        {
            InitializeComponent();
            _price = price;
        }

        private void buttonNumPad_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                textBoxPayment.Text += button.Text;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBoxPayment.Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            double payment;
            if (double.TryParse(textBoxPayment.Text, out payment))
            {
                var change = payment - _price;
                //TODO;
            }
        }
    }
}

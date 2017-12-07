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
        private decimal _price;
        public bool CheckOutSuccess { get; set; }

        public FormCheckout(decimal price)
        {
            InitializeComponent();
            _price = price;
            textBoxTotalPrice.Text = _price.ToString("c2");
        }

        private void buttonNumPad_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if ((button.Text == @",") && (textBoxPayment.Text.IndexOf('.') > -1))
                {
                    textBoxPayment.Text += button.Text;
                }
                else
                {
                    textBoxPayment.Text += button.Text;
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBoxPayment.Clear();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            decimal payment;
            var paymentStr = textBoxPayment.Text;
            if (decimal.TryParse(textBoxPayment.Text, out payment))
            {
                var change = payment - _price;
                if (change >= 0)
                {
                    textBoxChange.Text = change.ToString("c2");
                    CheckOutSuccess = true;
                    return;
                }
                else
                {
                    textBoxChange.Text = @"Gezahlter Preis zu niedrig";
                    CheckOutSuccess = false;
                    return;
                }
            }
            textBoxChange.Text = @"Falscher Preis angegeben";
            CheckOutSuccess = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            CheckOutSuccess = false;
            this.Close();
        }

        private void textBoxPayment_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

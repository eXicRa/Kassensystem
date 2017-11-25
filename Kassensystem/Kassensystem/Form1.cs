using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DataAccess;

namespace Kassensystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var productgroups = Productgroup.GetAll();


            int i = 0;
            foreach (var item in productgroups)
            {
                CustomButton b = new CustomButton();
                b.Id = item.Id;
                b.FlatStyle = FlatStyle.Flat;
                b.Text = item.Description;
                b.Size = new Size(150, 75);
                b.Location = new Point(175 * i, 0);
                b.Click += showProductsToProductgroup;

                panelProductgroups.Controls.Add(b);
                i++;
            }
        }


        public void showProductsToProductgroup(object sender, EventArgs e)
        {
            var button = sender as CustomButton;
            if (button != null)
            {
                Productgroup pg = new Productgroup(button.Id);
                pg.GetAllProducts();
                foreach (var item in pg.Products)
                {
                    listView1.Items.Add(item.ToString());
                }

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {

        }
    }
}

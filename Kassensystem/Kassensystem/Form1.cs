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
                Button b = new Button();
                b.Text = item.Description;
                b.Location = new Point(100 * i, 0);

                panel3.Controls.Add(b);
                i++;
            }
        }


        private void button14_Click(object sender, EventArgs e)
        {

        }
    }
}

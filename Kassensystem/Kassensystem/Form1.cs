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
            Product p = new Product(4);
        }


        private void button14_Click(object sender, EventArgs e)
        {
            
        }
    }
}

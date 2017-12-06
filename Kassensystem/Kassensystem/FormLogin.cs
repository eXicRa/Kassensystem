using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;

namespace Kassensystem
{
    public partial class FormLogin : Form
    {
        private TextBox _currentTextBox;
        public Employee Employee { get; set; }
        public bool LoginScuccess { get; set; }

        public FormLogin()
        {
            InitializeComponent();
            _currentTextBox = textBoxUser;
            LoginScuccess = false;
            label1.Text = "";
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            int user;
            int pin;

            if (int.TryParse(textBoxUser.Text, out user) && int.TryParse(textBoxPin.Text, out pin))
            {
                var result = Employee.LoginEmpleyee(user, pin);
                if (result != null)
                {
                    //logged in
                    Employee = result;
                    LoginScuccess = true;
                    this.Close();
                }
            }

            label1.Text = @"Falsche Logindaten";
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
            {
                _currentTextBox = tb;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                if (_currentTextBox != null)
                {
                    _currentTextBox.Text += button.Text;
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (_currentTextBox != null)
            {
                _currentTextBox.Clear();
            }
        }
    }
}

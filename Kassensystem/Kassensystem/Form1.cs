
using System;
using System.Drawing;
using System.Windows.Forms;
using CustomComponents;
using DataAccess;
using Kassensystem.Properties;

namespace Kassensystem
{
    public partial class Form1 : Form
    {
        private ShoppingCart _shoppingCart;
        private Employee _employee;

        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Hide();

            using (FormLogin login = new FormLogin())
            {
                login.ShowDialog();
                if (login.LoginScuccess)
                {
                    _employee = login.Employee;

                    Show();
                    WindowState = FormWindowState.Maximized;

                    _shoppingCart = new ShoppingCart(textBoxTotalAmount);
                    panel3.Visible = false;
                    labelMwst.Text = "";
                    radioButtonLocal.Checked = true;

                    var productgroups = Productgroup.GetAll();

                    int i = 0;
                    foreach (var item in productgroups)
                    {
                        CustomButton b = new CustomButton();
                        b.Obj = item;
                        b.FlatStyle = FlatStyle.Flat;
                        b.Text = item.Description;
                        b.Size = new Size(150, 75);
                        b.Location = new Point(175 * i, 0);
                        b.Click += ShowProductsToProductgroup;

                        panelProductgroups.Controls.Add(b);
                        i++;
                    }
                }
                else
                {
                    Close();
                }
            }


        }

        public void ShowProductsToProductgroup(object sender, EventArgs e)
        {
            var button = sender as CustomButton;
            if (button != null)
            {
                var progroup = button.Obj as Productgroup;
                if (progroup != null)
                {
                    flowLayoutPanelProducts.Controls.Clear();
                    Productgroup pg = new Productgroup(progroup.Id);
                    pg.GetAllProducts();
                    foreach (var item in pg.Products)
                    {
                        //flowLayoutPanelProducts
                        CustomButton buttonProduct = new CustomButton();
                        buttonProduct.Obj = item;
                        buttonProduct.Size = new Size(100, 50);
                        buttonProduct.FlatStyle = FlatStyle.Flat;
                        buttonProduct.BackColor = Color.Orange;
                        buttonProduct.Text = $@"{item.Description} {item.Price}€";
                        buttonProduct.Click += buttonProduct_Click;
                        flowLayoutPanelProducts.Controls.Add(buttonProduct);
                    }
                }
            }
        }

        private void buttonProduct_Click(object sender, EventArgs e)
        {
            var cusButton = sender as CustomButton;
            if (cusButton != null)
            {
                var product = cusButton.Obj as Product;
                if (product != null)
                {
                    ShoppingCartItem item;
                    bool isNewProduct;
                    int amount;
                    if (int.TryParse(tb_NumpadDisplay.Text, out amount))
                    {
                        item = _shoppingCart.AddProduct(product, out isNewProduct, amount);
                    }
                    else
                    {
                        item = _shoppingCart.AddProduct(product, out isNewProduct);
                    }
                    if (isNewProduct)
                    {
                        GenerateOrderPosition(item);
                    }
                    tb_NumpadDisplay.Clear();
                }
            }
        }

        private void GenerateOrderPosition(ShoppingCartItem item)
        {
            CustomButton minusButton = new CustomButton();

            minusButton.FlatStyle = FlatStyle.Flat;
            minusButton.Location = new Point(192, 13);
            minusButton.Size = new Size(32, 26);
            minusButton.TabIndex = 2;
            minusButton.Text = @"-";
            minusButton.UseVisualStyleBackColor = true;
            minusButton.Obj = item;
            minusButton.Click += _shoppingCart.Alter;

            CustomButton plusButton = new CustomButton();

            plusButton.FlatStyle = FlatStyle.Flat;
            plusButton.Location = new Point(317, 13);
            plusButton.Size = new Size(32, 26);
            plusButton.TabIndex = 3;
            plusButton.Text = @"+";
            plusButton.UseVisualStyleBackColor = true;
            plusButton.Obj = item;
            plusButton.Click += _shoppingCart.Alter;

            Label productLabel = new Label();

            productLabel.AutoSize = true;
            productLabel.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            productLabel.Location = new Point(6, 15);
            productLabel.Name = "label1";
            productLabel.Size = new Size(75, 20);
            productLabel.TabIndex = 0;
            var price = item.orderposition.Amount * item.orderposition.Product.Price;
            productLabel.Text = price.ToString("c2") + $@" {item.orderposition.Product}";

            TextBox amountTextBox = new TextBox();

            amountTextBox.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            amountTextBox.Location = new Point(230, 13);
            amountTextBox.Size = new Size(81, 26);
            amountTextBox.TabIndex = 1;
            amountTextBox.Text = item.orderposition.Amount.ToString();
            amountTextBox.TextAlign = HorizontalAlignment.Center;

            CustomButton buttonTrash = new CustomButton();

            buttonTrash.BackColor = Color.Transparent;
            buttonTrash.BackgroundImage = Resources.trash;
            buttonTrash.BackgroundImageLayout = ImageLayout.Zoom;
            buttonTrash.FlatStyle = FlatStyle.Popup;
            buttonTrash.Location = new Point(355, 13);
            buttonTrash.Size = new Size(32, 26);
            buttonTrash.TabIndex = 4;
            buttonTrash.UseVisualStyleBackColor = false;
            buttonTrash.Obj = item;
            buttonTrash.Click += buttonTrash_Click;

            item.buttonMinus = minusButton;
            item.buttonPlus = plusButton;
            item.label = productLabel;
            item.textBox = amountTextBox;

            CustomPanel panel = new CustomPanel();

            panel.Controls.Add(minusButton);
            panel.Controls.Add(plusButton);
            panel.Controls.Add(amountTextBox);
            panel.Controls.Add(productLabel);
            panel.Controls.Add(buttonTrash);
            panel.Location = new Point(3, 20);
            panel.Size = new Size(398, 53);

            item.panel = panel;

            flowLayoutPanelShoppingCart.Controls.Add(panel);
        }

        private void buttonTrash_Click(object sender, EventArgs e)
        {
            var button = sender as CustomButton;
            if (button != null)
            {
                var item = button.Obj as ShoppingCartItem;
                if (item != null)
                {
                    _shoppingCart.DeleteProduct(item);
                }
            }
        }

        private void flowLayoutPanelProducts_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button_NumpadClick(object sender, EventArgs e)
        {
            var buttonVar = sender as Button;
            if (buttonVar != null)
            {
                tb_NumpadDisplay.Text += buttonVar.Text;
            }
        }

        private void button_ClearNumpad(object sender, EventArgs e)
        {
            tb_NumpadDisplay.Clear();
        }

        private void radioButtonLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLocal.Checked)
            {
                labelMwst.Text = @"19%";
                _shoppingCart.MwSt = 19;
                _shoppingCart.CalculateTotalAmount();
            }
            else
            {
                labelMwst.Text = @"7%";
                _shoppingCart.MwSt = 7;
                _shoppingCart.CalculateTotalAmount();
            }
        }



        private void button17_Click(object sender, EventArgs e)
        {
            _shoppingCart.SaveToDatabase();
        }


    }
}


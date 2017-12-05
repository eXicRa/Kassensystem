
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
using CustomComponents;

namespace Kassensystem
{
    public partial class Form1 : Form
    {
        private ShoppingCart shoppingCart;

        public Form1()
        {
            InitializeComponent();

            shoppingCart = new ShoppingCart(textBoxTotalAmount);
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
                        buttonProduct.Text = $"{item.Description} {item.Price}€";
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
                        item = shoppingCart.AddProduct(product, out isNewProduct, amount);
                    }
                    else
                    {
                        item = shoppingCart.AddProduct(product, out isNewProduct);
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

            minusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            minusButton.Location = new System.Drawing.Point(192, 13);
            minusButton.Size = new System.Drawing.Size(32, 26);
            minusButton.TabIndex = 2;
            minusButton.Text = "-";
            minusButton.UseVisualStyleBackColor = true;
            minusButton.Obj = item;
            minusButton.Click += shoppingCart.Alter;

            CustomButton plusButton = new CustomButton();

            plusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            plusButton.Location = new System.Drawing.Point(317, 13);
            plusButton.Size = new System.Drawing.Size(32, 26);
            plusButton.TabIndex = 3;
            plusButton.Text = "+";
            plusButton.UseVisualStyleBackColor = true;
            plusButton.Obj = item;
            plusButton.Click += shoppingCart.Alter;

            Label productLabel = new Label();

            productLabel.AutoSize = true;
            productLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            productLabel.Location = new System.Drawing.Point(6, 15);
            productLabel.Name = "label1";
            productLabel.Size = new System.Drawing.Size(75, 20);
            productLabel.TabIndex = 0;
            var price = item.orderposition.Amount * item.orderposition.Product.Price;
            productLabel.Text = price.ToString("c2") + $" {item.orderposition.Product}";

            TextBox amountTextBox = new TextBox();

            amountTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            amountTextBox.Location = new System.Drawing.Point(230, 13);
            amountTextBox.Size = new System.Drawing.Size(81, 26);
            amountTextBox.TabIndex = 1;
            amountTextBox.Text = item.orderposition.Amount.ToString();
            amountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            CustomButton buttonTrash = new CustomButton();

            buttonTrash.BackColor = System.Drawing.Color.Transparent;
            buttonTrash.BackgroundImage = Properties.Resources.trash;
            buttonTrash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            buttonTrash.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            buttonTrash.Location = new System.Drawing.Point(355, 13);
            buttonTrash.Size = new System.Drawing.Size(32, 26);
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
            panel.Location = new System.Drawing.Point(3, 20);
            panel.Size = new System.Drawing.Size(398, 53);

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
                    shoppingCart.DeleteProduct(item);
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {

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
                labelMwst.Text = "19%";
                shoppingCart.MwSt = 19;
                shoppingCart.CalculateTotalAmount();
            }
            else
            {
                labelMwst.Text = "7%";
                shoppingCart.MwSt = 7;
                shoppingCart.CalculateTotalAmount();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            shoppingCart.SaveToDatabase();
        }
    }
}


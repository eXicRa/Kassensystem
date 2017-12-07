using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public class Product
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public decimal Price { get; set; }
        public Productgroup Productgroup { get; set; }
        List<Discount> Discount { get; set; }


        public Product()
        {

        }

        //Johann,Nils
        public Product(int id)
        {
            MySqlParameter mySqlParameter = new MySqlParameter("@id", id);
            var reader = Database.ExcecuteCommand("SELECT * FROM Produkt where ID = @id", new List<MySqlParameter> { mySqlParameter });

            while (reader.Read())
            {
                Id = reader.GetInt32(0);
                Description = reader.GetString(1);
                Price = reader.GetDecimal(2);
            }
        }

        //Johann,Nils
        public static List<Product> GetAll()
        {
            var reader = Database.ExcecuteCommand("SELECT * FROM Produkt");
            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product produkt = new Product();

                produkt.Id = reader.GetInt32(0);
                produkt.Description = reader.GetString(1);
                produkt.Price = reader.GetDecimal(2);

                products.Add(produkt);
            }
            return products;
        }

        public override string ToString()
        {
            return Description;
        }

    }
}

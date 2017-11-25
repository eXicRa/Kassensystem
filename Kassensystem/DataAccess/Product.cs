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
        public double Price { get; set; }
        public Productgroup Productgroup { get; set; }
        List<Discount> Discount { get; set; }


        public Product()
        {

        }

        public Product(int id)
        {
            MySqlParameter mySqlParameter = new MySqlParameter("@id", id);
            var reader = Database.ExcecuteCommand("SELECT * FROM Produkt where ID = @id", new List<MySqlParameter> { mySqlParameter });

            while (reader.Read())
            {
                Id = reader.GetInt32(0);
                Description = reader.GetString(1);
                Price = reader.GetDouble(2);
            }
        }

        public static List<Product> GetAll()
        {
            var reader = Database.ExcecuteCommand("SELECT * FROM Produkt");
            List<Product> products = new List<Product>();

            while (reader.Read())
            {
                Product produkt = new Product();

                produkt.Id = reader.GetInt32(0);
                produkt.Description = reader.GetString(1);
                produkt.Price = reader.GetDouble(2);

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

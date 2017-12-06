using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public class Productgroup
    {
        public int Id { get; set; }
        public String Description { get; set; }
        public List<Product> Products { get; set; }

        public Productgroup()
        {

        }

        //Johann,Nils
        public Productgroup(int id)
        {
            MySqlParameter mySqlParameter = new MySqlParameter("@id", id);
            var reader = Database.ExcecuteCommand("SELECT * FROM Produktgruppe where ID = @id", new List<MySqlParameter> { mySqlParameter });

            while (reader.Read())
            {
                Id = reader.GetInt32(0);
                Description = reader.GetString(1);
            }
        }

        //Johann,Nils
        public static List<Productgroup> GetAll()
        {
            var reader = Database.ExcecuteCommand("SELECT * FROM Produktgruppe");
            List<Productgroup> productgroups = new List<Productgroup>();

            while (reader.Read())
            {
                Productgroup productgroup = new Productgroup();

                productgroup.Id = reader.GetInt32(0);
                productgroup.Description = reader.GetString(1);

                productgroups.Add(productgroup);
            }
            return productgroups;
        }

        //Johann,Nils
        public void GetAllProducts()
        {
            MySqlParameter para = new MySqlParameter("@id", this.Id);
            var reader = Database.ExcecuteCommand("SELECT * FROM Produkt where FK_Produktgruppe_ID = @id", new List<MySqlParameter> { para });
            Products = new List<Product>();

            while (reader.Read())
            {
                Product produkt = new Product();

                produkt.Id = reader.GetInt32(0);
                produkt.Description = reader.GetString(1);
                produkt.Price = reader.GetDouble(2);

                Products.Add(produkt);
            }
        }
    }
}

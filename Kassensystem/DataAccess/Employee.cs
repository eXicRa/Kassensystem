using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Employee
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int Pin { get; set; }
        public Employee Boss { get; set; }

        public static void LoginEmpleyee(int id, int pin)
        {
            MySql.Data.MySqlClient.MySqlParameter idPara = new MySql.Data.MySqlClient.MySqlParameter("@id", id);
            MySql.Data.MySqlClient.MySqlParameter pinPara = new MySql.Data.MySqlClient.MySqlParameter("@pin", pin);
            var reader = Database.ExcecuteCommand("SELECT * FROM mitarbeiter WHERE id = @id and Pin = @pin", new List<MySql.Data.MySqlClient.MySqlParameter> { idPara, pinPara });
            if (!reader.HasRows)
            {
                // login falsch
                return;
            }

            while (reader.Read())
            {
                // Employee Bauen
            }
        }
    }
}

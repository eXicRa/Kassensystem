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

        //Johann,Nils
        public static Employee LoginEmpleyee(int id, int pin)
        {
            MySql.Data.MySqlClient.MySqlParameter idPara = new MySql.Data.MySqlClient.MySqlParameter("@id", id);
            MySql.Data.MySqlClient.MySqlParameter pinPara = new MySql.Data.MySqlClient.MySqlParameter("@pin", pin);
            var reader = Database.ExcecuteCommand("SELECT * FROM mitarbeiter WHERE id = @id and PIN = @pin", new List<MySql.Data.MySqlClient.MySqlParameter> { idPara, pinPara });
            if (!reader.HasRows)
            {
                // login falsch
                return null;
            }


            Employee employee = new Employee();
            employee.Id = id;

            reader.Read();

            // Employee Bauen
            employee.FirstName = reader[1].ToString();
            employee.LastName = reader[2].ToString();

            return employee;
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    public static class Database
    {
        private readonly static string connectionString = "SERVER=localhost;" +
                            "DATABASE=Kassensystem;" +
                            "UID=root;" +
                            "PASSWORD=;";

        private static MySqlConnection _sqlConnection;

        private static MySqlConnection GetSqlConnection()
        {
            if (_sqlConnection == null)
            {
                _sqlConnection = new MySqlConnection(connectionString);
                _sqlConnection.Open();
            }
            return _sqlConnection;
        }

        public static MySqlDataReader ExcecuteCommand(string sql, List<MySqlParameter> parameter = null)
        {
            var sqlConnection = GetSqlConnection();
            MySqlCommand command = sqlConnection.CreateCommand();
            command.CommandText = sql;
            if (parameter != null)
            {
                foreach (var sqlParameter in parameter)
                {
                    command.Parameters.Add(sqlParameter);
                }
            }
            MySqlDataReader reader;
            reader = command.ExecuteReader();
            return reader;
        }


    }


}

﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql;
using MySql.Data.MySqlClient;

namespace DataAccess
{
    //Johann,Nils
    public static class Database
    {
        private static readonly string connectionString = "SERVER=localhost;" +
                            "DATABASE=Kassensystem;" +
                            "UID=itt35;" +
                            "PASSWORD=itt35;";

        private static MySqlConnection _sqlConnection;
        private static MySqlDataReader reader;

        //Johann,Nils
        private static MySqlConnection GetSqlConnection()
        {
            if (_sqlConnection == null)
            {
                _sqlConnection = new MySqlConnection(connectionString);
                _sqlConnection.Open();
            }
            return _sqlConnection;
        }

        //Johann,Nils
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

            if (reader != null && !reader.IsClosed)
            {
                reader.Close();
            }
            reader = command.ExecuteReader();
            return reader;
        }


    }


}

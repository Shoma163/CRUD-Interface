using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace CRUD_Interface
{
    public class Connection
    {
        public static NpgsqlConnection connection;
        public static void Connect(string host, string port, string user, string pass, string dbname)
        {
            string cs = $"Server={host};Port={port};User ID={user};Password={pass};DataBase={dbname}";

            connection = new NpgsqlConnection(cs);
            connection.Open();
        }

        public static ObservableCollection<ClassProduct> products { get; set; } = new ObservableCollection<ClassProduct>();

        public static NpgsqlCommand GetCommand(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = sql;
            return command;
        }

        public static void SelectTableProduct()
        {
            NpgsqlCommand cmd = GetCommand("SELECT \"id\",\"name\" FROM \"product\"");
            NpgsqlDataReader result = cmd.ExecuteReader();

            if (result.HasRows)
            {
                while (result.Read())
                {
                    products.Add(new ClassProduct(result.GetInt32(0), result.GetString(1)));
                }

            }
            result.Close();
        }
    }
}

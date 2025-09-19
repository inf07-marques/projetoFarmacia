using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProjetoFarmacia
{
    public class Database
    {
        private static string connString =
            System.Configuration.ConfigurationManager
            .ConnectionStrings["MySqlConn"].ConnectionString;

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connString);
        }
    }
}
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModel
{
    public class CommonVariables
    {
        public static string ConnectionString { get; set; }
        public static MySqlConnection GetSqlConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
        public static int SqlCommandTimeout
        {
            get
            {
                return 90;
            }
        }
    }
}

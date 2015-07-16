using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace gemi.data
{
    public class Data
    {
        string CS = "server=localhost;user id=root;password=test;database=gemi_test";
        MySqlConnection con;

        public Data()
        {
            con = new MySqlConnection(CS);
        }

        public bool Open()
        {
            con.Open();
            return true;
        }

        public bool Close()
        {
            con.Close();
            return true;
        }
    }
}

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
        protected MySqlConnection con;

        public Data()
        {
            con = new MySqlConnection(CS);
        }

        /// <summary>
        /// MySql veritabanı bağlantısını açar.
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            con.Open();
            return true;
        }
        /// <summary>
        /// MySql veritabanı ile olan bağlantıyı kapatır.
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            try
            {
                con.Close();
            }
            catch (MySqlException)
            {
                return false;
            }
            return true;
        }
        
    }
}

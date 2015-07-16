using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gemi.Entities;
using MySql.Data.MySqlClient;

namespace gemi.data
{
    public class UserData : gemi.data.Data
    {
        /*public bool CheckUserRole(string username, string role)
        {
            string query = "select role from user where username=@username";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);

            OpenConnection();
            bool result = (role == Convert.ToString(cmd.ExecuteScalar()));
            CloseConnection();

            return result;
        }*/

        public bool LoginUser(User user)
        {
            string query = "select password from user where username=@username";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username",user.username);
            
            Open();
            bool result = (user.password == Convert.ToString(cmd.ExecuteScalar()));
            Close();

            return result;
        }

        public bool AddUser(User user)
        {
            string query = "insert into user (username,password) values (@username,@password)";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", user.username);
            cmd.Parameters.AddWithValue("@password", user.password);

            Open();
            cmd.ExecuteNonQuery();
            Close();

            return true;
        }

        public List<User> GetUsers()
        {
            string query = "select * from user";

            List<User> users = new List<User>();

            MySqlCommand cmd = new MySqlCommand(query, con);

            Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                User user = new User();
                user.username = Convert.ToString(reader["username"]);
                user.password = Convert.ToString(reader["password"]);
                users.Add(user);
            }
            Close();

            return users;
        }

        public bool ChangePassword(string username, string password)
        {
            string query = "update user set password=@password where username=@username";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            Open();
            cmd.ExecuteNonQuery();
            Close();

            return true;
        }

        public bool RemoveUser(string username)
        {
            string query = "delete from user where username=@username";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);

            Open();
            cmd.ExecuteNonQuery();
            Close();

            return true;
        }

        public bool ChangeName(string oldname, string newname)
        {
            string query = "update user set username=@newname where username=@oldname";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@newname", newname);
            cmd.Parameters.AddWithValue("@oldname", oldname);

            Open();
            cmd.ExecuteNonQuery();
            Close();

            return true;
        }
        

    }
}

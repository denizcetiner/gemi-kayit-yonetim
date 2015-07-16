using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using gemi.Entities;

namespace gemi.data
{
    public class RolesData : Data
    {
        public string GetRole(string username)
        {
            string query = "select role from roles where username=@username";
            MySqlCommand cmd = new MySqlCommand(query,con);
            cmd.Parameters.AddWithValue("@username",username);

            Open();
            string result = Convert.ToString(cmd.ExecuteScalar());
            Close();

            return result;
        }

        public List<Roles> GetRoles()
        {
            List<Roles> roles = new List<Roles>();

            string query = "select * from roles";

            MySqlCommand cmd = new MySqlCommand(query, con);

            Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Roles role = new Roles();
                role.username = Convert.ToString(reader["username"]);
                role.role = Convert.ToString(reader["role"]);
                roles.Add(role);
            }
            Close();

            return roles;
        }

        public bool SetRole(string username,string role)
        {
            string query = "update roles set role=@role where username=@username";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@username", username);

            Open();
            cmd.ExecuteNonQuery();
            Close();

            return true;
        }

        public bool CheckRole(string username, string[] roles)
        {
            string query = "select role from roles where username=@username";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);

            Open();
            string role = Convert.ToString(cmd.ExecuteScalar());
            Close();

            bool result = false;

            foreach(string role_ in roles)
            {
                result = result || (role == role_);
            }

            return result;
        }

        public bool RemoveUser(string username)
        {
            string query = "delete from roles where username=@username";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);

            Open();
            cmd.ExecuteNonQuery();
            Close();

            return true;
        }

        public bool AddUser(string username, string role)
        {
            string query = "insert into roles (username,role) values(@username,@role)";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@role", role);

            Open();
            cmd.ExecuteNonQuery();
            Close();

            return true;
        }

        public bool ChangeUserName(string oldname, string newname)
        {
            string query = "update roles set username=@newname where username=@oldname";

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

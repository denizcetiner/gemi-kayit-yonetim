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

        /// <summary>
        /// Kullanıcı rollerinin tutulduğu veritabanından, parametre olarak gönderilmiş kullanıcı adına
        /// ait rolü çeker.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Kullanıcının sahip olduğu rolü geri döndürür.</returns>
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

        /// <summary>
        /// Roller veritabanına bağlanarak kullanıcı adlarını ve rollerini alır.
        /// </summary>
        /// <returns>Rollerin tam listesini geri döndürür.</returns>
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

        /// <summary>
        /// Parametre olarak yollanan kullanıcı adına, parametre olarak yollanan rolü atar.
        /// </summary>
        /// <param name="username">Kullanıcı adı.</param>
        /// <param name="role">Kullanıcıya verilecek yeni rol.</param>
        /// <returns></returns>
        public void SetRole(string username,string role)
        {
            string query = "update roles set role=@role where username=@username";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@username", username);

            Open();
            cmd.ExecuteNonQuery();
            Close();
        }

        /// <summary>
        /// Bir kullanıcı adının, verilen rollerden bir veya birden fazlasına sahip olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="username">Kullanıcı adı</param>
        /// <param name="roles">Kullanıcı rolleri</param>
        /// <returns></returns>
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

        /// <summary>
        /// Kullanıcıyı roller veritabanından siler
        /// </summary>
        /// <param name="username">Kullanıcı adı</param>
        /// <returns></returns>
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

        /// <summary>
        /// Roller veritabanına yeni bir kullanıcı ve rolünü ekler
        /// </summary>
        /// <param name="username"></param>
        /// <param name="role"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Roller veritabanında bir kullanıcının kullanıcı adını değiştirir.
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        /// <returns></returns>
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

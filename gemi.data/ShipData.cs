using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using gemi.Entities;

namespace gemi.data
{
    public class ShipData : gemi.data.Data
    {

        /// <summary>
        /// Gemi kayıtları veritabanına yeni bir kayıt ekler.
        /// </summary>
        /// <param name="ship">Ship sınıfından bir nesne</param>
        /// <returns></returns>
        public void AddGemi(gemi.Entities.Ship ship)
        {
            string query = "insert into ship (ref_id,ship_id,date_time,description,created_by,created_pc,created_datetime) values (@ref_id,@ship_id,@date_time,@description,@created_by,@created_pc,@created_datetime)";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ref_id", ship.refId);
            cmd.Parameters.AddWithValue("@ship_id", ship.shipId);
            cmd.Parameters.AddWithValue("@date_time", ship.time);
            cmd.Parameters.AddWithValue("@description", ship.description);
            cmd.Parameters.AddWithValue("@created_by", ship.createdBy);
            cmd.Parameters.AddWithValue("@created_datetime", ship.createdDatetime);
            cmd.Parameters.AddWithValue("@created_pc", ship.createdPc);

            Open();
            cmd.ExecuteNonQuery();
            Close();
        }

        /// <summary>
        /// Gemi veritabanından, Referans ID'si verilen bir geminin istenen özelliklerini, Ship sınıfından bir nesneye atar.
        /// </summary>
        /// <param name="ref_id"></param>
        /// <returns>İstenen özelliklerle birlikte bir Ship nesnesi döndürür.</returns>
        public Ship GetShip(string ref_id)
        {
            Ship ship = new Ship();

            string query = "select * from ship where ref_id = @ref_id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ref_id", ref_id);

            Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                ship.refId = Convert.ToString(reader["ref_id"]);
                ship.shipId = Convert.ToInt32(reader["ship_id"]);
                ship.time = Convert.ToDateTime(reader["date_time"]);
                ship.description = Convert.ToString(reader["description"]); 
            }
            Close();

            return ship;
        }


        /// <summary>
        /// Bir kullanıcının girdiği tüm gemi kayıtlarını çeker.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Ship listesi döndürür.</returns>
        public List<Ship> GetRecords(string username)
        {
            string query = "select ref_id,ship_id,date_time,description from ship where created_by=@created_by";

            List<Ship> records = new List<Ship>();

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@created_by", username);

            Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Ship ship = new Ship();
                ship.refId = Convert.ToString(reader["ref_id"]);
                ship.shipId = Convert.ToInt32(reader["ship_id"]);
                ship.time = Convert.ToDateTime(reader["date_time"]);
                ship.description = Convert.ToString(reader["description"]);
                records.Add(ship);
            }
            Close();

            return records;
        }

        /// <summary>
        /// Bir gemi kaydına ait bilgileri günceller.
        /// </summary>
        /// <param name="ship"></param>
        /// <returns></returns>
        public void UpdateShip(Ship ship)
        {
            string query = "update ship set ship_id=@ship_id,date_time=@date_time,description=@description,updated_by=@updated_by,updated_pc=@updated_pc,updated_datetime=@updated_datetime where ref_id=@ref_id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ship_id", ship.shipId);
            cmd.Parameters.AddWithValue("@date_time", ship.time);
            cmd.Parameters.AddWithValue("@description", ship.description);
            cmd.Parameters.AddWithValue("@updated_by", ship.updatedBy);
            cmd.Parameters.AddWithValue("@updated_pc", ship.updatedPc);
            cmd.Parameters.AddWithValue("@updated_datetime", ship.updatedDatetime);
            cmd.Parameters.AddWithValue("@ref_id",ship.refId);

            Open();
            cmd.ExecuteNonQuery();
            Close();
        }

        /// <summary>
        /// Referans id'si verilen bir gemi kaydının kimin tarafından yapıldığını döndürür.
        /// </summary>
        /// <param name="ref_id"></param>
        /// <returns>Kullanıcı adını döndürür.</returns>
        public string GetCreatedBy(string ref_id)
        {
            string query = "select created_by from ship where ref_id=@ref_id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ref_id", ref_id);

            Open();
            string result = Convert.ToString(cmd.ExecuteScalar());
            Close();

            return result;
        }

        /// <summary>
        /// Gemi tanımı ID'sine sahip olan gemileri siler.
        /// </summary>
        /// <param name="ship_id"></param>
        /// <returns></returns>
        public void DeleteShips(int ship_id)
        {
            string query = "delete from ship where ship_id=@ship_id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ship_id", ship_id);

            Open();
            cmd.ExecuteNonQuery();
            Close();
        }

        /// <summary>
        /// Bir gemi tanımı ID'sine sahip gemilerin referanslarını çeker.
        /// </summary>
        /// <param name="ship_id"></param>
        /// <returns>Gemi referans ID'lerini string listesi olarak döndürür.</returns>
        public List<string> GetShipReferencesOfName(int ship_id)
        {
            List<string> references = new List<string>();
            string reference;

            string query = "select ref_id from ship where ship_id=@ship_id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ship_id", ship_id);

            Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                reference = Convert.ToString(reader["ref_id"]);
                references.Add(reference);
            }
            Close();

            return references;
        }

        /// <summary>
        /// Database'de verilen referans ID'sine sahip bir satırın olup olmadığını döndürür.
        /// </summary>
        /// <param name="ref_id"></param>
        /// <returns>Satır varsa true, yok ise false döndürür.</returns>
        public bool CheckIfExists(string ref_id)
        {
            string query = "select count(*) from ship where ref_id=@ref_id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ref_id", ref_id);

            Open();
            bool result = (Convert.ToInt32(cmd.ExecuteScalar()) != 0);
            Close();

            return result;
        }


        /// <summary>
        /// Bir tanım ID'sine sahip gemileri döndürür.
        /// </summary>
        /// <param name="ship_id"></param>
        /// <returns>Ship sınıfından nesnelerin listesini döndürür.</returns>
        public List<Ship> GetShipsByName(int ship_id)
        {
            List<Ship> ships = new List<Ship>();
            string query = "select ref_id,date_time from ship where ship_id=@ship_id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ship_id", ship_id);

            Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Ship ship = new Ship();
                ship.refId = Convert.ToString(reader["ref_id"]);
                ship.shipId = ship_id;
                ship.time = Convert.ToDateTime(reader["date_time"]);
                ships.Add(ship);
            }
            Close();
            return ships;
        }

        /// <summary>
        /// Başlangıç ve bitiş tarihi verilen gemi kayıtlarını döndürür.
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns>Ship sınıfından nesnelerin listesini döndürür.</returns>
        public List<Ship> GetShipsBetweenDate(DateTime begin, DateTime end)
        {
            List<Ship> ships = new List<Ship>();

            string query = "select ref_id,ship_id,date_time from ship where date_time between @begin and @end";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@begin", begin);
            cmd.Parameters.AddWithValue("@end", end);

            Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Ship ship = new Ship();
                ship.refId = Convert.ToString(reader["ref_id"]);
                ship.shipId = Convert.ToInt32(reader["ship_id"]);
                ship.time = Convert.ToDateTime(reader["date_time"]);
                ships.Add(ship);
            }
            Close();
            return ships;
        }

        /// <summary>
        /// Bir tarih aralığı ve tanım ID'si verilen gemilerin listesini döndürür.
        /// </summary>
        /// <param name="ship_id"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns>Ship sınıfından nesneler döndürür.</returns>
        public List<Ship> GetShipsByDateAndName(int ship_id, DateTime begin, DateTime end)
        {
            List<Ship> ships = new List<Ship>();

            string query = "select ref_id,date_time from ship where (ship_id=@ship_id) and (date_time between @begin and @end)";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ship_id", ship_id);
            cmd.Parameters.AddWithValue("@begin", begin);
            cmd.Parameters.AddWithValue("@end", end);

            Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Ship ship = new Ship();
                ship.refId = Convert.ToString(reader["ref_id"]);
                ship.time = Convert.ToDateTime(reader["date_time"]);
                ship.shipId = ship_id;
                ships.Add(ship);
            }
            Close();
            return ships;
        }
    }
}

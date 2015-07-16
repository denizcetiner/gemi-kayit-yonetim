using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using gemi.Entities;

namespace gemi.data
{
    public class ShipUrlData : gemi.data.Data
    {
        public List<ShipUrl> GetPhotos(string refId)
        {
            List<ShipUrl> urlList = new List<ShipUrl>();

            string query = "select id,image_url,preview from photos where ship_id = @ship_id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ship_id", refId);

            Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ShipUrl shipUrl = new ShipUrl();
                shipUrl.refId = refId;
                shipUrl.id = Convert.ToInt32(reader["id"]);
                shipUrl.preview = (byte[])reader["preview"];
                shipUrl.imageUrl = Convert.ToString(reader["image_url"]);

                urlList.Add(shipUrl);
            }
            Close();

            return urlList;
        }

        public bool AddShipUrl(ShipUrl shipUrl)
        {
            string query = "insert into photos (ship_id,image_url,preview) values (@ship_id,@image_url,@preview)";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ship_id",shipUrl.refId);
            cmd.Parameters.AddWithValue("@image_url",shipUrl.imageUrl);
            cmd.Parameters.AddWithValue("@preview", shipUrl.preview);

            Open();
            cmd.ExecuteNonQuery();
            Close();

            return true;
        }

        public bool DeletePicture(int id)
        {
            string query = "delete from photos where id=@id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            Open();
            cmd.ExecuteNonQuery();
            Close();

            return true;
        }

        public bool DeletePicturesOfShip(string ref_id)
        {
            string query = "delete from photos where ship_id=@ship_id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ship_id", ref_id);

            Open();
            cmd.ExecuteNonQuery();
            Close();

            return true;
        }


        public bool CheckDuplicateUrl(string ship_ref_id, string filename, string path)
        {
            string query = "select count(*) image_url from photos where (ship_id=@ship_id and image_url=@image_url)";

            MySqlCommand cmd = new MySqlCommand(query,con);
            cmd.Parameters.AddWithValue("@ship_id",ship_ref_id);
            cmd.Parameters.AddWithValue("@image_url",path+filename);

            Open();
            bool result = Convert.ToBoolean((Convert.ToInt32(cmd.ExecuteScalar()) != 0));
            Close();

            return result;
        }

        public string GetFilePath(int id)
        {
            string query = "select image_url from photos where id=@id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            Open();
            string result = cmd.ExecuteScalar().ToString();
            Close();

            return result;
        }

        public List<string> GetFilePaths(string ref_id)
        {
            List<string> filepaths = new List<string>();

            string query = "select image_url from photos where ship_id=@ship_id";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ship_id", ref_id);

            Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                filepaths.Add(Convert.ToString(reader["image_url"]));
            }
            Close();

            return filepaths;
        }

    }
}

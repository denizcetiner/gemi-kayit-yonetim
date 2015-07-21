using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gemi.Entities;
using MySql.Data.MySqlClient;

namespace gemi.DAL
{
    public class TanimData : gemi.DAL.Data
    {

        /// <summary>
        /// Tanımları, ID'leri ile birlikte bir sözlük olarak döndürür.
        /// </summary>
        /// <returns>Dictionary int string, int=tanimID,string=tanim</returns>
        public Dictionary<int,string> GetTanimlar()
        {
            Dictionary<int, string> tanimlar = new Dictionary<int, string>();

            string query = "select * from tanimlar";

            MySqlCommand cmd = new MySqlCommand(query, con);

            Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tanimlar.Add(Convert.ToInt32(reader["tanim_id"]),Convert.ToString(reader["tanim"]));
            }
            Close();

            return tanimlar;
        }

        /// <summary>
        /// Bir tanım ekler.
        /// </summary>
        /// <param name="tanim"></param>
        /// <returns></returns>
        public void TanimEkle(string tanim)
        {
            string query = "insert into tanimlar (tanim) values (@tanim)";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@tanim",tanim);

            Open();
            cmd.ExecuteNonQuery();
            Close();
        }

        /// <summary>
        /// Bir gemi tanımını siler.
        /// </summary>
        /// <param name="tanim"></param>
        public void DeleteTanim(string tanim)
        {
            string query = "delete from tanimlar where tanim=@tanim";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@tanim", tanim);

            Open();
            cmd.ExecuteNonQuery();
            Close();

        }

        /// <summary>
        /// Tanımın ID'sini döndürür.
        /// </summary>
        /// <param name="tanim"></param>
        /// <returns></returns>
        public int getTanimId(string tanim)
        {
            string query = "select tanim_id from tanimlar where tanim=@tanim";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@tanim", tanim);

            Open();
            int tanimId = Convert.ToInt32(cmd.ExecuteScalar());
            Close();

            return tanimId;
        }

        /// <summary>
        /// Bir ID'nin tanımını döndürür.
        /// </summary>
        /// <param name="tanim_id"></param>
        /// <returns></returns>
        public string getTanim(int tanim_id)
        {
            string query = "select tanim from tanimlar where tanim_id=@tanim_id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@tanim_id", tanim_id);

            Open();
            string tanim = Convert.ToString(cmd.ExecuteScalar());
            Close();

            return tanim;
        }

        /// <summary>
        /// ID'si verilen kayıt silinir.
        /// </summary>
        /// <param name="tanim_id"></param>
        public void DeleteTanimById(int tanim_id)
        {
            string query = "delete from tanimlar where tanim_id=@tanim_id";
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@tanim_id", tanim_id);

            Open();
            cmd.ExecuteNonQuery();
            Close();
        }

        /// <summary>
        /// Veritabanındaki tüm tanımları Tanimlar listesi halinde döndürür.
        /// </summary>
        /// <returns></returns>
        public List<Tanimlar> GetAllTanimlar()
        {
            List<Tanimlar> tanimlar = new List<Tanimlar>();
            

            string query = "select tanim,tanim_id from tanimlar";

            MySqlCommand cmd = new MySqlCommand(query, con);

            Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Tanimlar tanim = new Tanimlar();
                tanim.tanim = Convert.ToString(reader["tanim"]);
                tanim.tanimId = Convert.ToInt32(reader["tanim_id"]);
                tanimlar.Add(tanim);
            }
            Close();

            return tanimlar;
        }

        public bool CheckIfExists(string name)
        {
            string query = "select count(*) tanim from tanimlar where tanim = @tanim";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@tanim", name);

            Open();
            bool result = (Convert.ToInt32(cmd.ExecuteScalar()) == 1);
            Close();

            return result;
        }

        public void EditName(int tanimId,string oldname, string name)
        {
            string query = "update tanimlar set tanim=@tanim where tanim_id = @tanim_id and tanim=@eski";

            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.Parameters.AddWithValue("@eski", oldname);
            cmd.Parameters.AddWithValue("@tanim",name);
            cmd.Parameters.AddWithValue("tanim_id",tanimId);

            Open();
            cmd.ExecuteNonQuery();
            Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace gemi.OtherMethods
{
    public class FileMethods
    {

        /// <summary>
        /// Blob'u (byte dizisini) Image sınıfından bir nesneye dönüştürür.
        /// </summary>
        /// <param name="blob"></param>
        /// <returns>Image sınıfından bir nesne döndürür.</returns>
        public System.Drawing.Image BlobToImage(byte[] blob)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(blob);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }

        /// <summary>
        /// Upload edilen bir dosyayı (HttpPostedFileBase sınıfından oluşan), Image'a çevirir.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Image sınıfından bir nesne döndürür.</returns>
        public System.Drawing.Image FileToImage(System.Web.HttpPostedFileBase file)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(file.InputStream);
            return image;
        }

        /// <summary>
        /// Bir Image nesnesini, byte dizisine çevirir.
        /// </summary>
        /// <param name="image"></param>
        /// <returns>Byte dizisi döndürür.</returns>
        public byte[] ImageToBytes(System.Drawing.Image image)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            
            return ms.ToArray();
        }

        /// <summary>
        /// Bir image'ı, server'da verilen dosya yoluna, verilen dosya adı ile kaydeder.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public void SaveFile(System.Drawing.Image image, string path, string filename)
        {
            image.Save(path + filename, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        /// <summary>
        /// Server'da, istenen bir klasörün fiziksel yolunu string olarak döndürür.
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public string GetPath(string folderName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + folderName + "/");
            
            return path;
        }

        /// <summary>
        /// Server'da, verilen dosya yolunda, verilen dosya adına sahip bir dosya adının varlığını kontrol eder.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <returns>Dosya varsa 'true', yoksa 'false' döndürür.</returns>
        public bool FileExists(string path, string filename)
        {
            //string[] files = System.IO.Directory.GetFiles(path + filename);
            bool result = File.Exists(path + filename);  //(files != null);
            
            return result;
        }

        /// <summary>
        /// Bir dosya adı stringini, sonuna rasgele bir karakter ekleyerek değiştirir.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string renameFile(string path, string filename)
        {
            string extension = System.IO.Path.GetExtension(path + filename);

            if (extension != "")
            {
                filename = filename.Substring(0, filename.Length - extension.Length);
                filename = filename + GetRandomChar();
                return filename + extension;
            }
            else
            {
                filename = filename + GetRandomChar();
                return filename;    
            }
        }

        /// <summary>
        /// Alfanumerik bir karakter topluluğundan rasgele bir karakter çeker.
        /// </summary>
        /// <returns></returns>
        public char GetRandomChar()
        {
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random rand = new Random();
            return chars[rand.Next(0, chars.Length)];
        }

        /// <summary>
        /// Dosya yolu verilen bir dosyayı, eğer var ise siler.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>Dosya varsa true, yok ise false döndürür.</returns>
        public bool DeleteFile(string filepath)
        {
            bool result = File.Exists(filepath);
            if(File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            return result;
        }

        /// <summary>
        /// Serverdaki bir dosya ile, post edilmiş ancak henüz kaydedilmemiş bir dosyanın boyutunu karşılaştırır.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="filepath"></param>
        /// <returns>Boyut aynı ise 'true', farklı ise 'false' döndürür.</returns>
        public bool CompareSizeOfFile(System.Web.HttpPostedFileBase file, string filepath)
        {
            FileInfo info = new FileInfo(filepath);
            return (info.Length == file.ContentLength);
        }

        /*public bool CompareCreationTime(System.Web.HttpPostedFileBase file, string filepath) //Upload edilen dosyanın oluşturulma zamanı görülemiyor ama tekrar araştır
        {
            FileInfo info = new FileInfo(filepath);
            return (info.CreationTime == file.InputStream.
        }*/

    }
}

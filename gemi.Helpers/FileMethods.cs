using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace gemi.Helpers
{
    public class FileMethods
    {
        public System.Drawing.Image BlobToImage(byte[] blob)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(blob);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }

        public System.Drawing.Image FileToImage(System.Web.HttpPostedFileBase file)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(file.InputStream);
            return image;
        }

        public byte[] ImageToBytes(System.Drawing.Image image)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            
            return ms.ToArray();
        }

        public bool SaveFile(System.Drawing.Image image, string path, string filename)
        {
            image.Save(path + filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            
            return true;
        }

        public string GetPath(string folderName)
        {
            string path = System.Web.HttpContext.Current.Server.MapPath("~/" + folderName + "/");
            
            return path;
        }

        public bool FileExists(string path, string filename)
        {
            //string[] files = System.IO.Directory.GetFiles(path + filename);
            bool result = File.Exists(path + filename);  //(files != null);
            
            return result;
        }

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

        public char GetRandomChar()
        {
            string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            Random rand = new Random();
            return chars[rand.Next(0, chars.Length)];
        }

        public bool DeleteFile(string filepath)
        {
            bool result = File.Exists(filepath);
            if(File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            return true;
        }

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

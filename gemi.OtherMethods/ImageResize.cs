using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace gemi.OtherMethods
{
    public class ImageResize
    {

        /// <summary>
        /// Bir Image'ın boyutlarını değiştirir.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns>Boyutları değiştirilmiş yeni image'ı döndürür.</returns>
        public System.Drawing.Image Resize(System.Drawing.Image image, int width, int height)
        {
            System.Drawing.Image newImage = image.GetThumbnailImage(width, height, null, new IntPtr());
            return newImage;
        }
    }
}

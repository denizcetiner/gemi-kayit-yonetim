using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace gemi.Helpers
{
    public class ImageResize
    {
        public System.Drawing.Image Resize(System.Drawing.Image image, int width, int height)
        {
            System.Drawing.Image newImage = image.GetThumbnailImage(width, height, null, new IntPtr());
            return newImage;
        }
    }
}

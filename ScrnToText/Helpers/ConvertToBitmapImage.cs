using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ScrnToText.Helpers
{
    public class ConvertToBitmapImage
    {
        public BitmapImage ConvertToBitmap(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new())
            {
                // Save the bitmap to a memory stream
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                memoryStream.Position = 0;

                // Create a new BitmapImage
                BitmapImage bitmapImage = new();

                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}

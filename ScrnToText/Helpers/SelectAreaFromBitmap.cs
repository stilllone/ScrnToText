using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ScrnToText.Helpers
{
    public class SelectAreaFromBitmap
    {
        public Bitmap SelectAreaByRect(Bitmap sourceBitmap, Rect rect)
        {
            Rectangle drawingRect = new Rectangle((int)rect.Left, (int)rect.Top, (int)rect.Width, (int)rect.Height);
            Bitmap selectedBitmap = new Bitmap(drawingRect.Width, drawingRect.Height);

            using (Graphics graphics = Graphics.FromImage(selectedBitmap))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(sourceBitmap, new Rectangle(0, 0, drawingRect.Width, drawingRect.Height), drawingRect, GraphicsUnit.Pixel);
            }

            return selectedBitmap;
        }
    }
}

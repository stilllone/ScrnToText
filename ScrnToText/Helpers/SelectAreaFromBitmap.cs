using System;
using System.Drawing;
using System.Windows;

namespace ScrnToText.Helpers
{
    public class SelectAreaFromBitmap : IDisposable
    {
        private Bitmap? selectedBitmap;
        private bool disposed = false;
        public Bitmap SelectAreaByRect(Bitmap sourceBitmap, Rect rect)
        {
            Rectangle drawingRect = new Rectangle((int)rect.Left, (int)rect.Top, (int)rect.Width, (int)rect.Height);
            selectedBitmap = new Bitmap(drawingRect.Width, drawingRect.Height);

            using (Graphics graphics = Graphics.FromImage(selectedBitmap))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(sourceBitmap, new Rectangle(0, 0, drawingRect.Width, drawingRect.Height), drawingRect, GraphicsUnit.Pixel);
            }

            return selectedBitmap;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    selectedBitmap?.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

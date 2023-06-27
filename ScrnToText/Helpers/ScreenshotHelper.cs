using ScrnToText.DataProvider;
using ScrnToText.Views.ScreenHandler;
using System;
using Point = System.Drawing.Point;
using System.Drawing;
using System.Windows;

namespace ScrnToText.Helpers
{
    public class ScreenshotHelper : IDisposable
    {
        private Bitmap? bitmap;
        private Bitmap? selectedBitmap;
        private readonly ImageToTextConverter imageToTextConverter = new();
        private readonly GetDataToTesseract getDataToTesseract = new();
        private readonly SelectAreaFromBitmap selectAreaFromBitmap = new();
        private readonly ConvertToBitmapImage convertToBitmapImage = new();
        private Rect selectedArea;
        private bool disposed = false;

        public string? GetScreenshot(Rectangle bounds, int maxHeight, string currentLanguage)
        {
            // Create a image with size that user specifies
            bitmap = new Bitmap(bounds.Width, maxHeight);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                using (ScreenSelectionControl selectionForm = new (convertToBitmapImage.ConvertToBitmap(bitmap)))
                {
                    selectionForm.ShowDialog();
                    selectionForm.Dispose();

                    selectedArea = selectionForm.SelectedArea;
                    if (selectedArea != new Rect(0, 0, 0, 0))
                    {
                        // Image to text
                        selectedBitmap = selectAreaFromBitmap.SelectAreaByRect(bitmap, selectedArea);
                        getDataToTesseract.GetCurrentLangData(currentLanguage, out string dataPath, out string shortLang);
                        return imageToTextConverter.GetTextFromImage(selectedBitmap, dataPath, shortLang);
                    }
                }
            }
            return default;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    bitmap?.Dispose();
                    selectedBitmap?.Dispose();
                    selectedArea = Rect.Empty;
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

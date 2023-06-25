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
        private Bitmap bitmap;
        private ScreenSelectionControl selectionForm;
        private Bitmap selectedBitmap;
        private ImageToTextConverter imageToTextConverter = new();
        private GetDataToTesseract getDataToTesseract = new();
        private SelectAreaFromBitmap selectAreaFromBitmap = new();

        public string? GetScreenshot(Rectangle bounds, int maxHeight, string currentLanguage)
        {
            // Create a image with size that user specifies
            bitmap = new Bitmap(bounds.Width, maxHeight);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
            }

            ConvertToBitmapImage convertToBitmapImage = new ConvertToBitmapImage();
            selectionForm = new ScreenSelectionControl(convertToBitmapImage.ConvertToBitmap(bitmap));
            selectionForm.ShowDialog();
            Rect selectedArea = selectionForm.SelectedArea;

            if (selectedArea != new Rect(0, 0, 0, 0))
            {
                // Image to text
                selectedBitmap = selectAreaFromBitmap.SelectAreaByRect(bitmap, selectedArea);
                getDataToTesseract.GetCurrentLangData(currentLanguage, out string dataPath, out string shortLang);
                return imageToTextConverter.GetTextFromImage(selectedBitmap, dataPath, shortLang);
            }
            return default;
        }

        public void Dispose()
        {
            bitmap?.Dispose();
            selectionForm?.Dispose();
            selectedBitmap?.Dispose();
        }
    }

}

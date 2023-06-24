using System.Drawing;
using Tesseract;

namespace ScrnToText.DataProvider
{
    public class ImageToTextConverter
    {
        public string GetTextFromImage(Bitmap bitmap, string dataPath, string shortLang)
        {
            using (TesseractEngine? engine = new TesseractEngine(dataPath, shortLang, EngineMode.Default))
            {
                using (Page? page = engine.Process(PixConverter.ToPix(bitmap), PageSegMode.Auto))
                {
                    return page.GetText();
                }
            }
        }
    }
}

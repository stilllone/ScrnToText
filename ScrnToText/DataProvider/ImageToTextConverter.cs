using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace ScrnToText.DataProvider
{
    public class ImageToTextConverter
    {
        public string GetTextFromImage(Bitmap bitmap, string currLang)
        {
            using (TesseractEngine? engine = new TesseractEngine(@"", currLang, EngineMode.Default))
            {
                using (Page? page = engine.Process(PixConverter.ToPix(bitmap), PageSegMode.Auto))
                {
                    return page.GetText();
                }
            }
            return default;
        }
    }
}

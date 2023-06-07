using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace ScrnToText.DataProvider
{
    public class ImageToTextConverter
    {
        public string GetTextFromImage()
        {
            using (var engine = new TesseractEngine(@"path_to_lang", "eng", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(@"path_to_image"))//need to update to screen and selection
                {
                    using (var page = engine.Process(img))
                    {
                        return page.GetText();
                    }
                }
            }
        }
    }
}

using ScrnToText.Helpers;

namespace ScrnToText.DataProvider
{
    public class GetDataToTesseract
    {
        public void GetCurrentLangData(string currLang, out string dataPath, out string shortLang)
        {
            switch (currLang)
            {
                case nameof(LangEnum.English):
                    dataPath = @"./tessdata";
                    shortLang = "eng";
                    break;
                case nameof(LangEnum.Ukrainian):
                    dataPath = @"./tessdata";
                    shortLang = "ukr";
                    break;
                case nameof(LangEnum.German):
                    dataPath = @"./tessdata";
                    shortLang = "deu";
                    break;
                default:
                    dataPath = default;
                    shortLang = default;
                    break;
            }
        }
    }
}

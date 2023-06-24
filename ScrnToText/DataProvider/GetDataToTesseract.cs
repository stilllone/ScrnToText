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
                    shortLang = currLang.Substring(0, 3).ToLower();
                    break;
                case nameof(LangEnum.Ukrainian):
                    dataPath = "/tessdata/ukr.traineddata";
                    shortLang = dataPath.Split('/')[2].Split('.')[0];
                    break;
                case nameof(LangEnum.German):
                    dataPath = "/tessdata/deu.traineddata";
                    shortLang = dataPath.Split('/')[2].Split('.')[0];
                    break;
                default:
                    dataPath = default;
                    shortLang = default;
                    break;
            }
        }
    }
}

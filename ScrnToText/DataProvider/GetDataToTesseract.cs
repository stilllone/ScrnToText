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
                    dataPath = "/TesseractData/eng.traineddata";
                    shortLang = dataPath.Split('/')[2].Split('.')[0];
                    break;
                case nameof(LangEnum.Ukrainian):
                    dataPath = "/TesseractData/ukr.traineddata";
                    shortLang = dataPath.Split('/')[2].Split('.')[0];
                    break;
                case nameof(LangEnum.German):
                    dataPath = "/TesseractData/deu.traineddata";
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

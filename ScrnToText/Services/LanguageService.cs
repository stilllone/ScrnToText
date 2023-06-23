using CommunityToolkit.Mvvm.ComponentModel;
using ScrnToText.Helpers;
using ScrnToText.Interface;

namespace ScrnToText.Services
{
    public partial class LanguageService : ObservableRecipient, ILanguageService
    {
        public LanguageService() 
        {
            Language = LangEnum.English.ToString();
        }

        [ObservableProperty]
        private string _language;

        public string GetCurrentLanguage()
        {
            if (Language != null)
                return Language;
            return null;
        }

        public void SetLanguage(string lang)
        {
            Language = lang;
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ScrnToText.Helpers;
using ScrnToText.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Wpf.Ui.Common.Interfaces;

namespace ScrnToText.ViewModels
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {

        private string language;
        public string Language 
        { 
            get
            { 
                return language; 
            } 
            set 
            {  
                language = value;
                LanguageService.SetLanguage(language);
                OnPropertyChanged(nameof(Language));
            } 
        }

        [ObservableProperty]
        private List<string> _supportedLanguages;

        private bool _isInitialized = false;

        [ObservableProperty]
        private string _appVersion = String.Empty;

        [ObservableProperty]
        private Wpf.Ui.Appearance.ThemeType _currentTheme = Wpf.Ui.Appearance.ThemeType.Unknown;

        [ObservableProperty]
        private ILanguageService _languageService;

        public SettingsViewModel(ILanguageService languageService)
        {
            LanguageService = languageService;
            SupportedLanguages = Enum.GetValues(typeof(LangEnum)).Cast<LangEnum>().Select(v => v.ToString()).ToList();
        }

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom()
        {
        }

        private void InitializeViewModel()
        {
            CurrentTheme = Wpf.Ui.Appearance.Theme.GetAppTheme();
            AppVersion = $"ScrnToText - {GetAssemblyVersion()}";

            _isInitialized = true;
        }

        private string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? String.Empty;
        }

        [RelayCommand]
        private void OnChangeTheme(string parameter)
        {
            switch (parameter)
            {
                case "theme_light":
                    if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Light)
                        break;

                    Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Light);
                    CurrentTheme = Wpf.Ui.Appearance.ThemeType.Light;

                    break;

                default:
                    if (CurrentTheme == Wpf.Ui.Appearance.ThemeType.Dark)
                        break;

                    Wpf.Ui.Appearance.Theme.Apply(Wpf.Ui.Appearance.ThemeType.Dark);
                    CurrentTheme = Wpf.Ui.Appearance.ThemeType.Dark;

                    break;
            }
        }
    }
}

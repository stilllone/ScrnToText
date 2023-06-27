using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using System.Drawing;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.Input;
using ScrnToText.Views.ScreenHandler;
using ScrnToText.Interface;
using ScrnToText.Helpers;
using System.Linq;

namespace ScrnToText.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _applicationTitle = String.Empty;

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationItems = new();

        [ObservableProperty]
        private ObservableCollection<INavigationControl> _navigationFooter = new();

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new();

        [ObservableProperty]
        private string _textFromTesseract;

        [ObservableProperty]
        private ILanguageService _languageService;

        public MainWindowViewModel(INavigationService navigationService, ILanguageService languageService)
        {
            if (!_isInitialized)
                InitializeViewModel();
            LanguageService = languageService;
        }

        private void InitializeViewModel()
        {
            ApplicationTitle = "WPF UI - ScrnToText";

            NavigationItems = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Home",
                    PageTag = "dashboard",
                    Icon = SymbolRegular.Home24,
                    PageType = typeof(Views.Pages.DashboardPage)
                }
            };

            NavigationFooter = new ObservableCollection<INavigationControl>
            {
                new NavigationItem()
                {
                    Content = "Settings",
                    PageTag = "settings",
                    Icon = SymbolRegular.Settings24,
                    PageType = typeof(Views.Pages.SettingsPage)
                }
            };

            TrayMenuItems = new ObservableCollection<MenuItem>
            {
                new MenuItem
                {
                    Header = "Home",
                    Tag = "tray_home"
                }
            };

            _isInitialized = true;
        }

        [RelayCommand]
        private void OnCapture()
        {
            using (var screenshotHelper = new ScreenshotHelper())
            {
                Screen[]? screens = Screen.AllScreens;
                Rectangle bounds = screens.Select(screen => screen.Bounds).Aggregate(Rectangle.Union);
                int maxHeight = screens.Max(screen => screen.Bounds.Height);
                TextFromTesseract = screenshotHelper.GetScreenshot(bounds, maxHeight, LanguageService.GetCurrentLanguage());
            }
        }
    }
}

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
using System.Windows;
using Point = System.Drawing.Point;
using System.Windows.Media.Imaging;
using System.IO;

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
        private ObservableCollection<Wpf.Ui.Controls.MenuItem> _trayMenuItems = new();

        public MainWindowViewModel(INavigationService navigationService)
        {
            if (!_isInitialized)
                InitializeViewModel();
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

            TrayMenuItems = new ObservableCollection<Wpf.Ui.Controls.MenuItem>
            {
                new Wpf.Ui.Controls.MenuItem
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
            int maxHeight = 0;
            Rectangle bounds = new();
            foreach (Screen screen in Screen.AllScreens)
            {
                bounds = Rectangle.Union(bounds, screen.Bounds);
                // If monitors with a different diagonal 
                if (screen.Bounds.Height > maxHeight)
                {
                    maxHeight = screen.Bounds.Height;
                }
            }
            GetScreenshot(bounds, maxHeight);
        }

        private void GetScreenshot(Rectangle bounds, int maxHeight)
        {
            // Create a image with size that user specifies
            using (Bitmap bitmap = new(bounds.Width, maxHeight))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                }
                ScreenSelectionControl selectionForm = new(ConvertToBitmapImage(bitmap));
                    selectionForm.ShowDialog();
                Rect selectedArea = selectionForm.SelectedArea;

                if (selectedArea != new Rect(0,0,0,0))
                {
                    // Save screenshot
                    System.Windows.Forms.Clipboard.SetImage(bitmap.Clone(ConvertRectToRectangle(selectedArea), bitmap.PixelFormat));
                }
            }
        }

        private BitmapImage ConvertToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the bitmap to a memory stream
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                memoryStream.Position = 0;

                // Create a new BitmapImage
                BitmapImage bitmapImage = new BitmapImage();

                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        private Rectangle ConvertRectToRectangle(Rect rect)
        {
            int x = (int)rect.X;
            int y = (int)rect.Y;
            int width = (int)rect.Width;
            int height = (int)rect.Height;

            return new Rectangle(x, y, width, height);
        }
    }
}

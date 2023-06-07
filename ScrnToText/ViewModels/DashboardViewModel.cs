using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Controls;
using Wpf.Ui.Common.Interfaces;

namespace ScrnToText.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _counter = 0;

        [ObservableProperty]
        private Image _imageHolder;




        [RelayCommand]
        private void OnCounterIncrement()
        {
            Counter++;
        }
    }
}

using System;
using System.Windows.Input;
using System.Windows;
using Point = System.Windows.Point;
using Brushes = System.Windows.Media.Brushes;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Media.Effects;

namespace ScrnToText.Views.ScreenHandler
{
    public partial class ScreenSelectionControl : Window, IDisposable
    {
        private event EventHandler<Rect> SelectionClosed;
        private Point _startPoint;

        private Rectangle rect;

        private Rect _selectedArea;
        public Rect SelectedArea => _selectedArea;

        private int x;
        private int y;
        private int width;
        private int height;

        private BitmapImage _screenshot;
        public BitmapImage Screenshot
        {
            get => _screenshot;
        }
        
        public ScreenSelectionControl(BitmapImage screenshot)
        {
            _screenshot = screenshot;
            ConfigureUserContorl();
            DataContext = this;
            InitializeComponent();
            Closing += OnClosing;
        }

        private void ConfigureUserContorl()
        {
            Left = SystemParameters.VirtualScreenLeft;
            Top = SystemParameters.VirtualScreenTop;
            Width = SystemParameters.VirtualScreenWidth;
            Height = SystemParameters.VirtualScreenHeight;
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            SelectionClosed?.Invoke(this, _selectedArea);
        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            // Calculate the selected area
            x = (int)Math.Min(e.GetPosition(this).X, _startPoint.X);
            y = (int)Math.Min(e.GetPosition(this).Y, _startPoint.Y);
            width = (int)Math.Abs(e.GetPosition(this).X - _startPoint.X);
            height = (int)Math.Abs(e.GetPosition(this).Y - _startPoint.Y);

            _selectedArea = new Rect(x, y, width, height);

            // Redraw to update the selection rectangle
            if (!(width <= 0 || height <= 0))
            {
                // Update selection rectangle
                rect.Width = width;
                rect.Height = height;

                Canvas.SetLeft(rect, x);
                Canvas.SetTop(rect, y);
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(canvas);
            rect = new Rectangle
            {
                Stroke = Brushes.LightBlue,
                StrokeThickness = 2
            };
            rect.Effect = null;
            // Draw start
            Canvas.SetLeft(rect, _startPoint.X);
            Canvas.SetTop(rect, _startPoint.Y);
            canvas.Children.Add(rect);
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
            GC.WaitForPendingFinalizers();
        }
    }
}
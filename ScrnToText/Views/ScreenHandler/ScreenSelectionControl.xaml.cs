using System;
using System.Windows.Input;
using System.Windows;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using Point = System.Windows.Point;
using System.Windows.Media;
using Pen = System.Windows.Media.Pen;
using Brushes = System.Windows.Media.Brushes;
using Brush = System.Windows.Media.Brush;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.ComponentModel;

namespace ScrnToText.Views.ScreenHandler
{
    /// <summary>
    /// Interaction logic for ScreenSelectionControl.xaml
    /// </summary>
    public partial class ScreenSelectionControl : Window
    {
        public event EventHandler<Rect> SelectionClosed;
        private Point _startPoint;
        private BitmapImage _screenshot;
        private Rectangle rect;
        private Rect _selectedArea;
        public Rect SelectedArea => _selectedArea;
        private int x;
        private int y;
        private int width;
        private int height;
        public BitmapImage Screenshot
        {
            get => _screenshot;
        }
        
        public ScreenSelectionControl(BitmapImage screenshot)
        {
            _screenshot = screenshot;
            ConfigureForm();
            DataContext = this;
            InitializeComponent();
            Closing += OnClosing;
        }

        private void ConfigureForm()
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

            // сalculate the selected area
            x = (int)Math.Min(e.GetPosition(this).X, _startPoint.X);
            y = (int)Math.Min(e.GetPosition(this).Y, _startPoint.Y);
            width = (int)Math.Abs(e.GetPosition(this).X - _startPoint.X);
            height = (int)Math.Abs(e.GetPosition(this).Y - _startPoint.Y);

            _selectedArea = new Rect(x, y, width, height);

            // redraw to update the selection rectangle
            if (!(width <= 0 || height <= 0))
            {
                // update selection rectangle
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
            // draw start
            Canvas.SetLeft(rect, _startPoint.X);
            Canvas.SetTop(rect, _startPoint.Y);
            canvas.Children.Add(rect);
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chain_of_Responsibilities
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Rectangle> rects = new List<Rectangle>();
        List<Ellipse> ellipses = new List<Ellipse>();

        public Shape figure;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MIDraw_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new Figure();
            wnd.Owner = this;
            wnd.ShowDialog();
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Shape newItem;
            if (figure is Ellipse)
            {
                newItem = new Ellipse
                {
                    Height = figure.Height,
                    Width = figure.Width
                };
                ellipses.Add(newItem as Ellipse);
            }
            else
            {
                newItem = new Rectangle
                {
                    Height = figure.Height,
                    Width = figure.Width
                };
                rects.Add(newItem as Rectangle);
            }

            Point start = e.GetPosition(canvas);
            newItem.Stroke = Brushes.Black;
            newItem.StrokeThickness = 1;
            Canvas.SetLeft(newItem, start.X - newItem.Width/2);
            Canvas.SetTop(newItem, start.Y - newItem.Height/2);
            canvas.Children.Add(newItem);
        }

        private void clearCanvas_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }

        private void MIAbout_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new About();
            wnd.ShowDialog();
        }
    }
}

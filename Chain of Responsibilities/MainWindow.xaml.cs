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
using MahApps.Metro.Controls;

namespace Chain_of_Responsibilities
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Shape figure;

        List<Command> commands = new List<Command>();
        int commandCounter = 0;

        public MainWindow()
        {
            InitializeComponent();

            Command.canvas = canvas;
        }

        private void MIDraw_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new Figure();
            wnd.Owner = this;
            wnd.ShowDialog();
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Command currentCommand;

            Point start = e.GetPosition(canvas);

            if (figure is Ellipse)
            {
                currentCommand = new EllipseCommand(figure, start);   
            }
            else if (figure is Rectangle)
            {
                currentCommand = new RectangleCommand(figure, start);
            }
            else //(figure is Triangle)
            {
                currentCommand = new TriangleCommand(figure, start);              
            }

            currentCommand.Execute();
            commands.Add(currentCommand);
            commandCounter++;
        }

        private void BClsCanvas_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
        }

        private void MIAbout_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new About();
            wnd.ShowDialog();
        }

        private void BUndo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BRedo_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

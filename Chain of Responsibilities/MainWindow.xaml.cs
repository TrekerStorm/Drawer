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
using System.Windows.Forms;
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
        Command currentCommand;
        int commandCounter = -1;

        Creator creator;

        public MainWindow()
        {
            InitializeComponent();

            Command.canvas = canvas;
        }

        private void MIDraw_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new Figure()
            {
                Owner = this
            };
            wnd.ShowDialog();
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point start = e.GetPosition(canvas);

            if (figure is Ellipse)
            {
                currentCommand = new EllipseCommand(figure, start);
                creator = new EllipseCreator(currentCommand);   
            }
            else if (figure is Rectangle)
            {
                currentCommand = new EllipseCommand(figure, start);
                creator = new RectangleCreator(currentCommand);
            }
            else //(figure is Triangle)
            {
                currentCommand = new TriangleCommand(figure, start);
                creator = new TriangleCreator(currentCommand);              
            }

            while (commands.Count - commandCounter > 1)
                commands.RemoveAt(commandCounter + 1);

            creator.Draw();

            commands.Add(currentCommand);
            commandCounter++;

            NUDShapeThickness.IsEnabled = true;
            BSelectColor.IsEnabled = true;
            BSelectThickness.IsEnabled = true;
        }

        private void BClsCanvas_Click(object sender, RoutedEventArgs e)
        {
            currentCommand = new ClearCanvasCommand();

            while (commands.Count - commandCounter > 1)
                commands.RemoveAt(commandCounter + 1);

            currentCommand.Execute();
            commands.Add(currentCommand);
            commandCounter++;

            NUDShapeThickness.IsEnabled = false;
            BSelectColor.IsEnabled = false;
            BSelectThickness.IsEnabled = false;
        }

        private void MIAbout_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new About();
            wnd.ShowDialog();
        }

        private void BUndo_Click(object sender, RoutedEventArgs e)
        {
            if (commandCounter > -1 && (commands.Count - commandCounter) < 4)
            {
                commands[commandCounter].UnExecute();
                commandCounter--;
            }

            if (canvas.Children.OfType<Shape>().Count() == 0)
            {
                NUDShapeThickness.IsEnabled = false;
                BSelectColor.IsEnabled = false;
                BSelectThickness.IsEnabled = false;
            }
            else
            {
                NUDShapeThickness.IsEnabled = true;
                BSelectColor.IsEnabled = true;
                BSelectThickness.IsEnabled = true;
            }
        }

        private void BRedo_Click(object sender, RoutedEventArgs e)
        {
            if((commands.Count - commandCounter < 5) && (commands.Count - commandCounter > 1))
            {
                commandCounter++;
                commands[commandCounter].Execute();
            }
            if (canvas.Children.OfType<Shape>().Count() > 0)
            {
                NUDShapeThickness.IsEnabled = true;
                BSelectColor.IsEnabled = true;
                BSelectThickness.IsEnabled = true;
            }
            else
            {
                NUDShapeThickness.IsEnabled = false;
                BSelectColor.IsEnabled = false;
                BSelectThickness.IsEnabled = false;
            }
        }

        private void BSelectThickness_Click(object sender, RoutedEventArgs e)
        {
            double oldThickness = canvas.Children.OfType<Shape>().First().StrokeThickness;
            double newThickness = NUDShapeThickness.Value ?? 0;

            currentCommand = new ThicknessCommand(newThickness, oldThickness);

            while (commands.Count - commandCounter > 1)
                commands.RemoveAt(commandCounter + 1);
            currentCommand.Execute();
            commands.Add(currentCommand);
            commandCounter++;

        }

        private void BSelectColor_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog wnd = new ColorDialog();
            Color color = new Color();
            Brush newBrush, oldBrush;

            wnd.ShowDialog();

            if (wnd.Color != null)
            {
                System.Drawing.Color color1 = wnd.Color;
                color.A = color1.A;
                color.R = color1.R; 
                color.G = color1.G;
                color.B = color1.B;

                newBrush = new SolidColorBrush(color);
                oldBrush = canvas.Children.OfType<Shape>().First().Stroke;

                CanvasLinesColor.Background = newBrush;

                currentCommand = new ColorCommand(newBrush, oldBrush);

                while (commands.Count - commandCounter > 1)
                    commands.RemoveAt(commandCounter + 1);
                currentCommand.Execute();
                commands.Add(currentCommand);
                commandCounter++;
            }
        }

        private void BMacroCommand_Click(object sender, RoutedEventArgs e)
        {
            double t = 1;
            Brush brush = Brushes.Black;

            List<Command> coms = new List<Command>
            {
                new RectangleCommand(
                    new Rectangle { Height = 40, Width = 60, StrokeThickness = t, Stroke = brush },
                    new Point(150, 100)
                    ),
                new EllipseCommand(
                    new Ellipse { Height = 20, Width = 100, StrokeThickness = t, Stroke = brush },
                    new Point(150, 300)
                    ),
                new TriangleCommand(
                    new Triangle { Height = 20, Width = 40, StrokeThickness = t, Stroke = brush },
                    new Point(250, 250)
                    ),
                new ThicknessCommand(4, t),
                new ColorCommand(Brushes.Green, brush)
            };
            Command macroCommand = new MacroCommand(coms);

            while (commands.Count - commandCounter > 1)
                commands.RemoveAt(commandCounter + 1);

            macroCommand.Execute();
            commands.Add(macroCommand);
            commandCounter++;

            if (canvas.Children.OfType<Shape>().Count() > 0)
            {
                NUDShapeThickness.IsEnabled = true;
                BSelectColor.IsEnabled = true;
                BSelectThickness.IsEnabled = true;
            }
            else
            {
                NUDShapeThickness.IsEnabled = false;
                BSelectColor.IsEnabled = false;
                BSelectThickness.IsEnabled = false;
            }
        }
    }
}

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
using System.Windows.Shapes;

namespace Chain_of_Responsibilities
{
    /// <summary>
    /// Interaction logic for Figure.xaml
    /// </summary>
    public partial class Figure : Window
    {
        public Shape f;
        Helper h1 = new BadShapeDimensionsHanadler(), 
               h2 = new UnSelectedShapeHandler();

        public Figure()
        {
            InitializeComponent();
        }

        private void BSettings_Click(object sender, RoutedEventArgs e)
        {
            //h1.Successor = h2;
            var success = h2.HandleRequest(f);
            if (success)
            {
                var wnd = new Settings()
                {
                    Owner = this
                };
                wnd.ShowDialog();
            }
        }

        private void BOk_Click(object sender, RoutedEventArgs e)
        {
            var mainWnd = this.Owner as MainWindow;
            if (mainWnd != null)
            {
                h1.Successor = h2;
                var success = h1.HandleRequest(f);

                if (success)
                {
                    mainWnd.figure = f;
                    this.Close();
                }
            }
        }

        private void CBFigure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBFigure.SelectedIndex == 0)
                f = new Ellipse();
            if (CBFigure.SelectedIndex == 1)
                f = new Rectangle();
            if (CBFigure.SelectedIndex == 2)
                f = new Triangle();

            f.Stroke = Brushes.Black;
            f.StrokeThickness = 1;
        }
    }
}

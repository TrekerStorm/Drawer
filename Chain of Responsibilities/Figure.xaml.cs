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

        public Figure()
        {
            InitializeComponent();
        }

        private void BSettings_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new Settings();
            wnd.Owner = this;
            wnd.ShowDialog();
        }

        private void BOk_Click(object sender, RoutedEventArgs e)
        {
            var mainWnd = this.Owner as MainWindow;
            if(mainWnd != null)
            {
                if (CBFigure.SelectedIndex == 0)
                    mainWnd.figure = f;
                else
                    mainWnd.figure = f;
            }
            this.Close();
        }

        private void CBFigure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBFigure.SelectedIndex == 0)
                f = new Ellipse();
            else
                f = new Rectangle();
        }
    }
}

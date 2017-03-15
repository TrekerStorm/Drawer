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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void BOk_Click(object sender, RoutedEventArgs e)
        {
            var mainWnd = this.Owner as Figure;
            if(mainWnd != null)
            {
                mainWnd.f.Height = Convert.ToInt32(TBHeight.Text);
                mainWnd.f.Width = Convert.ToInt32(TBWidth.Text);
            }
            this.Close();
        }
    }
}

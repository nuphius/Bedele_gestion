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
using BredeleGestion.Services;

namespace Bredele_Gestion
{
    /// <summary>
    /// Logique d'interaction pour ViewBoxPage.xaml
    /// </summary>
    public partial class ViewBoxPage : Page
    {
        ViewBoxService viewBox = new ViewBoxService();
        public ViewBoxPage()
        {
            InitializeComponent();
            this.DataContext = viewBox;
            //viewBox.LoadBoxBdd();

            //List<ViewBoxService> listBox = viewBox.LoadBoxBdd();

            //listViewBox.ItemsSource = listBox;
        }


        private void ModifyBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (int.TryParse(((Image)sender).Uid, out int id))
            {
                var mainWindow = Application.Current.MainWindow;
                var frameRight = mainWindow.FindName("FrameRight") as Frame;
                //var frameLeft = mainWindow.FindName("FrameLeft") as Frame;
                frameRight.Navigate(new AjoutLocauxPage(id));
                //rameLeft?.Navigate(new AdherentsPage());
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var a = ((Border)sender).Uid;
            MessageBox.Show("border !!" + a);
          
        }

        private void btnLock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (int.TryParse(((Image)sender).Uid, out int id))
            {
                var mainWindow = Application.Current.MainWindow;
                //var frameRight = mainWindow.FindName("FrameRight") as Frame;
                var frameLeft = mainWindow.FindName("FrameLeft") as Frame;
                //frameRight.Navigate(new AjoutLocauxPage(id));
                frameLeft.Navigate(new LockDatePage(id));
            }
        }
    }
}

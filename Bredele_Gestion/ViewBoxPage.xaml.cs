using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        static private int _idActivity;

        
        public ViewBoxPage(int idActivity = 0)
        {
            _idActivity = idActivity;
            ViewBoxService viewBox = new ViewBoxService(_idActivity);
            InitializeComponent();
            this.DataContext = viewBox;
            //this.DataContext = new ViewBoxViewModel();
        }

        private void ModifyBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (int.TryParse(((Image)sender).Uid, out int id))
            {
                var mainWindow = Application.Current.MainWindow;
                Frame frameRight = mainWindow.FindName("FrameRight") as Frame;
                Frame frameLeft = mainWindow.FindName("FrameLeft") as Frame;
                frameRight.Navigate(new AjoutLocauxPage(id));
                frameLeft.Navigate(new InfoAjoutLocauxPage());
            }
        }

        private void btnLock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (int.TryParse(((Image)sender).Uid, out int id))
            {
                var mainWindow = Application.Current.MainWindow;
                Frame frameLeft = mainWindow.FindName("FrameLeft") as Frame;
                frameLeft.Navigate(new LockDatePage(id));
            }
        }

        private void btnCalendar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (int.TryParse(((Image)sender).Uid, out int id))
            {
                var mainWindow = Application.Current.MainWindow;
                Frame frameRight = mainWindow.FindName("FrameRight") as Frame;
                frameRight.Navigate(new ViewBookPage(id));
            }
        }
    }
}

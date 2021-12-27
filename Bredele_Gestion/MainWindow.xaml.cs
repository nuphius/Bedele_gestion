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
using Bredele_Gestion.Controller;

namespace Bredele_Gestion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConnectionController connectionController = new ConnectionController();
        public MainWindow()
        {
            InitializeComponent();
            FrameLeft.Content = new AdherentsPage();
        }

        #region Evenement toolBar
        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            FrameLeft.Content = new AjoutAdherentPage();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            FrameLeft.Content = new AdherentsPage();
        }

        private void BtnAddTicket_Click(object sender, RoutedEventArgs e)
        {
            FrameLeft.Content = new AjoutReservationAdherentPage();
        }

        private void BtnAddBox_Click(object sender, RoutedEventArgs e)
        {
            FrameLeft.Content = new AjoutLocauxPage();
        }

        private void BtnAddPrice_Click(object sender, RoutedEventArgs e)
        {
            FrameLeft.Content = new AjoutTarifsPage();
        }
        #endregion

        private void MainWindows_Loaded(object sender, RoutedEventArgs e)
        {
            connectionController.Load();
        }

        private void BtnYesLogin_Click(object sender, RoutedEventArgs e)
        {
            string connectUser = connectionController.ConnectUser(TxtLoginConnection.Text, PwdConnection.Password);
            
            if (connectUser !="")
            {
                LblErrorConnection.Visibility = Visibility;
                LblErrorConnection.Content = connectUser;
            }
            else
            {
                BoxLoginConnection.Visibility = Visibility.Hidden;
            }
        }

        private void BtnNoLogin_Click(object sender, RoutedEventArgs e)
        {
            connectionController.Close();
            this.Close();
        }
    }
}

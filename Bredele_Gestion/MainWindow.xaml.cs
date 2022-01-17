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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConnectionController connectionController = new ConnectionController();
        public MainWindow()
        {
            InitializeComponent();
            FrameLeft.Navigate(new AdherentsPage());
            //FrameRight.Navigate(new AjoutLocauxPage());
        }

        #region Evenement toolBar
        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            FrameLeft.Navigate(new AdherentsPage());
            FrameRight.Navigate(new AjoutAdherentPage());
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            FrameLeft.Navigate(new AdherentsPage());
        }

        private void BtnAddTicket_Click(object sender, RoutedEventArgs e)
        {
            FrameLeft.Navigate(new AjoutReservationAdherentPage());
        }

        private void BtnAddBox_Click(object sender, RoutedEventArgs e)
        {
            FrameRight.Navigate(new AjoutLocauxPage());
            FrameLeft.Navigate(new InfoAjoutLocauxPage());
        }

        private void BtnAddPrice_Click(object sender, RoutedEventArgs e)
        {
            FrameRight.Navigate(new AjoutTarifsPage());
            FrameLeft.Navigate(new InfoAjoutTarifsPage());

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
                LblErrorConnection.Visibility = Visibility.Visible;
                LblErrorConnection.Content = connectUser;
            }
            else
            {
                BoxLoginConnection.Visibility = Visibility.Hidden;
                LblErrorConnection.Visibility = Visibility.Hidden;
                LblErrorConnection.Content = "";
            }
        }

        private void BtnNoLogin_Click(object sender, RoutedEventArgs e)
        {
            connectionController.Close();
            this.Close();
        }

        private void MainWindows_Closed(object sender, EventArgs e)
        {
            connectionController.Close();
            this.Close();
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            BoxLoginConnection.Visibility = Visibility.Visible;
            LblErrorConnection.Visibility = Visibility.Hidden;
            LblErrorConnection.Content = "";
            TxtLoginConnection.Clear();
            PwdConnection.Clear();
            FrameLeft.Navigate(new AdherentsPage());
        }
    }
}
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

namespace Bredele_Gestion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
    }
}

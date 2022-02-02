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
        ConnexionBddService connexionBdd = new ConnexionBddService();
        public MainWindow()
        {
            InitializeComponent();

            if (!connexionBdd.CheckDatabaseExists())
            {
                connexionBdd.CreateLocalDataBase();
            }

            FrameLeft.Navigate(new AdherentsPage());
            FrameRight.Navigate(new ViewBoxPage());
        }
        // Action des icones de la toolBar //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Evenement toolBar
        // Action lors du clic sur Home /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            FrameLeft.Navigate(new AdherentsPage());
            FrameRight.Navigate(new ViewBoxPage());
        }

        // Action lors du clic sur Ajout / Modification Personnes ///////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            FrameLeft.Navigate(new AdherentsPage());
            FrameRight.Navigate(new AjoutAdherentPage());
        }

        // Action lors du clic sur Ajout / Modification Réservations ////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnAddTicket_Click(object sender, RoutedEventArgs e)
        {
            FrameLeft.Navigate(new AjoutReservationAdherentPage());
            FrameRight.Navigate(new ViewBoxPage());
        }

        // Action lors du clic sur Ajout / Modification Locaux //////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnAddBox_Click(object sender, RoutedEventArgs e)
        {
            FrameRight.Navigate(new AjoutLocauxPage());
            FrameLeft.Navigate(new InfoAjoutLocauxPage());
        }

        // Action lors du clic sur Ajout / Modification Tarifs //////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnAddPrice_Click(object sender, RoutedEventArgs e)
        {
            FrameRight.Navigate(new AjoutTarifsPage());
            FrameLeft.Navigate(new InfoAjoutTarifsPage());

        }

        // Action lors du clic sur le bouton de Déconnexion /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            BoxLoginConnection.Visibility = Visibility.Visible;
            LblErrorConnection.Visibility = Visibility.Hidden;
            LblErrorConnection.Content = "";
            TxtLoginConnection.Clear();
            PwdConnection.Clear();
            FrameLeft.Navigate(new AdherentsPage());
        }
        #endregion
        
        // Action des boutons de la fenêtre de connexion ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Fenêtre de connexion à l'application
        // Indique que le logiciel se charge ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void MainWindows_Loaded(object sender, RoutedEventArgs e)
        {
            connectionController.Load();
        }

        // Intéraction lors du clic sur le bouton Valider ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

        // Intéraction lors du clic sur le bouton Annuler ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        #endregion
    }
}
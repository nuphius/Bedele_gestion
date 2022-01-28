using BredeleGestion.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bredele_Gestion
{
    /// <summary>
    /// Logique d'intéraction pour AjoutAdherentPage.xaml
    /// </summary>
    public partial class AjoutAdherentPage : Page
    {
        private int _idCust = 0;

        private string errorCp = string.Empty;
        private string errorPhone = string.Empty;
        private string errorBirthDay = string.Empty;
        private string errorMail = string.Empty;

        GestionAdherentsService adherentsService = new GestionAdherentsService();
        public AjoutAdherentPage(int idCust = 0)
        {
            InitializeComponent();
            this.DataContext = adherentsService;

            _idCust = idCust;

            if (_idCust != 0)
            {
                adherentsService.LoadUser(_idCust);
            }
        }

        // Paramètre du bouton Valider avec information si l'utilisateur est ajouté ou modifié //////////////////////////////////////////////////////////////////////////
        #region btnCustSub
        private void btnCustSub_Click(object sender, RoutedEventArgs e)
        {
            string error = adherentsService.CheckInfos();

            if (DisplayErrorForm(error))
            {
                string civility = cmbBoxCustCivility.Text;
                bool adherent = (chkBoxCustMember.IsChecked == true);

                if (adherentsService.AddUpdateUser(adherent, civility, _idCust))
                {
                    lblCustError.Foreground = new SolidColorBrush(Colors.Green); ;
                    if (_idCust != 0)
                    {
                        lblCustError.Content = "Utilisateur Modifié !";
                    }
                    else
                    {
                        lblCustError.Content = "Nouvel utilisateur ajouté !";
                    }
                    lblCustError.Visibility = Visibility.Visible;
                }

                var mainWindow = Application.Current.MainWindow;
                Frame frameLeft = mainWindow.FindName("FrameLeft") as Frame;
                frameLeft.Navigate(new AdherentsPage());

            }
        }
        #endregion

        // Paramètre du bouton Supprimer avec demande de confirmation de suppression ////////////////////////////////////////////////////////////////////////////////////
        #region btnCustDel
        private void btnCustDel_Click(object sender, RoutedEventArgs e)
        {
            if (_idCust !=0)
            {
                var warning = MessageBox.Show("Etes-vous sûr de vouloir supprimer cette utilisateur ?", "Suppression utilisateur", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (warning == MessageBoxResult.OK)
                {
                    adherentsService.DeleteUser(_idCust);
                    _idCust = 0;
                    CleanTxtBox();
                }
            }
        }
        #endregion

        // Message d'erreur si le champ code postal est mal renseigné ///////////////////////////////////////////////////////////////////////////////////////////////////
        #region txtBoxAddCP
        private void txtBoxAddCP_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxAddCP.Text.Length != 5)
            {
                errorCp = "- Code postal au mauvais format ! (5 chiffres!)\n";
                DisplayErrorForm();
            }
            else
            {
                errorCp = string.Empty;
                DisplayErrorForm();
            }
        }
        #endregion

        // Message d'erreur si le champ date de naissance est au mauvais format /////////////////////////////////////////////////////////////////////////////////////////
        #region txtBoxCustBirthDate
        private void txtBoxCustBirthDate_LostFocu(object sender, RoutedEventArgs e)
        {
            adherentsService.BirthDate = txtBoxCustBirthDate.Text;

            if (!adherentsService.birthDateFlag)
            {
                errorBirthDay = "- Date au mauvais format ! (JJ/MM/AAAA)\n";
                DisplayErrorForm();
            }
            else
            {
                errorBirthDay = string.Empty;
                DisplayErrorForm();
            }
        }
        #endregion

        // Message d'erreur si le champ téléphone est au mauvais format (10 chiffres) ///////////////////////////////////////////////////////////////////////////////////
        #region txtBoxCustPhone
        private void txtBoxCustPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBoxCustPhone.Text.Length != 10)
            {
                errorPhone = "- Téléphone au mauvais format ! (10 chiffres!)\n";
                DisplayErrorForm();
            }
            else
            {
                errorPhone = string.Empty;
                DisplayErrorForm();
            }
        }
        #endregion

        // Message d'erreur si le champ adresse email n'est pas conforme ////////////////////////////////////////////////////////////////////////////////////////////////
        #region txtBoxCustMail
        private void txtBoxCustMail_LostFocus(object sender, RoutedEventArgs e)
        {
            adherentsService.Mail = txtBoxCustMail.Text;

            if (!adherentsService.mailFlag)
            {
                errorMail = "- E-Mail au mauvais format !\n";
                DisplayErrorForm();
            }
            else
            {
                errorMail = string.Empty;
                DisplayErrorForm();
            }
        }
        #endregion

        // Affichage des messages d'erreur si erreur ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region DisplayErrorForm
        private bool DisplayErrorForm(string error = "")
        {
            lblCustError.Foreground = new SolidColorBrush(Colors.Red);
            lblCustError.Visibility = Visibility.Hidden;

            if (!string.IsNullOrEmpty(errorCp) || !string.IsNullOrEmpty(errorBirthDay) || !string.IsNullOrEmpty(errorPhone) || !string.IsNullOrEmpty(errorMail) || !string.IsNullOrEmpty(error))
            {
                lblCustError.Visibility = Visibility.Visible;
                lblCustError.Content = errorCp + errorBirthDay + errorPhone + errorMail + error;
                return false;
            }
            else
            {
                lblCustError.Visibility = Visibility.Hidden;
            }

            return true;
        }
        #endregion

        // Nettoie tous les champs quand un utilisateur est supprimé ////////////////////////////////////////////////////////////////////////////////////////////////////
        #region CleanTxtBox
        private void CleanTxtBox()
        {
            var mainWindow = Application.Current.MainWindow;
            var frameRight = mainWindow.FindName("FrameRight") as Frame;
            var frameLeft = mainWindow.FindName("FrameLeft") as Frame;
            frameRight?.Navigate(new AjoutAdherentPage());
            frameLeft?.Navigate(new AdherentsPage());
        }
        #endregion
    }
}
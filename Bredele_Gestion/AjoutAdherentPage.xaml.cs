using BredeleGestion.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Bredele_Gestion
{
    /// <summary>
    /// Logique d'interaction pour AjoutAdherentPage.xaml
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

            if (_idCust != 0)
            {
                adherentsService.LoadUser(_idCust);
            }
        }

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

            }
        }

        private void btnCustDel_Click(object sender, RoutedEventArgs e)
        {
            if (_idCust !=0)
            {
                var warning = MessageBox.Show("Etes-vous sûr de vouloir supprimer cette utilisateur ?", "Suppression utilisateur", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (warning == MessageBoxResult.OK)
                {
                    adherentsService.DeleteUser(_idCust);
                }
            }
        }

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
    }
}

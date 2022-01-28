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
    /// Logique d'interaction pour AjoutLocauxPage.xaml
    /// </summary>
    public partial class AjoutLocauxPage : Page
    {
        private int _idBox;

        GestionLocauxService gestionLocaux = new GestionLocauxService();
        public AjoutLocauxPage(int id = 0)
        {
            InitializeComponent();
            this.DataContext = gestionLocaux;

            _idBox = id;

            if (_idBox != 0)
                gestionLocaux.LoadBox(_idBox);
        }

        // Intéraction du bouton Valider la modification d'un local et message de retour ////////////////////////////////////////////////////////////////////////////////
        #region Bouton Valider
        private void btnBoxSub_Click(object sender, RoutedEventArgs e)
        {
            txtBoxError.Visibility = Visibility.Hidden;
            txtBoxError.Text = "";

            if (gestionLocaux.InsertUpdateBox(_idBox))
            {
                txtBoxError.Foreground = new SolidColorBrush(Colors.Green);

                if (_idBox != 0)
                {
                    txtBoxError.Text = "La Box a bien été modifié !";
                }
                else
                {
                    txtBoxError.Text = "La box a bien été ajouté !";
                }
                txtBoxError.Visibility = Visibility.Visible;
            }
            else
            {
                txtBoxError.Foreground = new SolidColorBrush(Colors.Red);
                txtBoxError.Visibility = Visibility.Visible;

                txtBoxError.Text = "Une erreur c'est produite !";
            }

            var mainWindow = Application.Current.MainWindow;
            var frameLeft = mainWindow.FindName("FrameLeft") as Frame;

            if (frameLeft != null)
            {
                frameLeft.Navigate(new InfoAjoutLocauxPage());
            }   
        }
        #endregion

        // Intéraction du bouton Supprimer un local et message de retour ////////////////////////////////////////////////////////////////////////////////////////////////
        #region Bouton Supprimer
        private void btnBoxDel_Click(object sender, RoutedEventArgs e)
        {
            if (_idBox != 0)
            {
                var warning = MessageBox.Show("Etes-vous sûr de vouloir supprimer ce local ?", "Suppression local", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (warning == MessageBoxResult.OK)
                {
                    gestionLocaux.DeleteBdd(_idBox, string.Format(RequetSqlService.DELETEEQUIPMENTBOXID, _idBox), RequetSqlService.TABLEEQUIPMENTBOX);
                    gestionLocaux.DeleteBdd(_idBox, string.Format(RequetSqlService.DELETEACTIVITYBOXID, _idBox), RequetSqlService.TABLEACTIVITYBOX);
                    gestionLocaux.DeleteBdd(_idBox, string.Format(RequetSqlService.DELETEBOX, _idBox), RequetSqlService.TABLEEQUIPMENTBOX);

                    _idBox = 0;

                    var mainWindow = Application.Current.MainWindow;
                    var frameLeft = mainWindow.FindName("FrameLeft") as Frame;
                    var frameRight = mainWindow.FindName("FrameRight") as Frame;

                    MessageBox.Show("Le local a bien été supprimé", "Suppression local", MessageBoxButton.OK, MessageBoxImage.Information);

                    if (frameLeft != null)
                        frameLeft.Navigate(new InfoAjoutLocauxPage());
                    if (frameRight != null)
                        frameRight.Navigate(new AjoutLocauxPage());

                }
            }
        }
        #endregion
    }
}

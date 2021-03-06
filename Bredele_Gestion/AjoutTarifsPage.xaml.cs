using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Logique d'interaction pour AjoutTarifsPage.xaml
    /// </summary>
    public partial class AjoutTarifsPage : Page
    {
        GestionTarifsService priceService = new GestionTarifsService();
        public AjoutTarifsPage()
        {
            InitializeComponent();
            this.DataContext = priceService;

            priceService.LoadListPrice();
        }

        // Intéraction du bouton Valider lors de l'ajout d'un nouveau tarif et message de retour ////////////////////////////////////////////////////////////////////////
        #region Bouton Valider
        private void btnAddPriceSub_Click_1(object sender, RoutedEventArgs e)
        {
            txtPriceError.Visibility = Visibility.Hidden;
            txtPriceError.Text = "";

            if (priceService.AddUpdatePrice())
            {
                txtPriceError.Foreground = new SolidColorBrush(Colors.Green);
                txtPriceError.Text = "Le nouveau tarif a bien été ajouté !";
                txtPriceError.Visibility = Visibility.Visible;

                RefreshFrame();
            }
            else
            {
                LogTools.AddLog(LogTools.LogType.ERREUR, "Problème lors je l'ajout d'un tarif dans la BDD");
            }

        }
        #endregion

        // Intéraction du bouton Modifier un tarif et message de retour /////////////////////////////////////////////////////////////////////////////////////////////////
        #region Bouton Modifier
        private void btnModPriceSub_Click(object sender, RoutedEventArgs e)
        {
            txtPriceError.Foreground = new SolidColorBrush(Colors.Red);
            txtPriceError.Visibility = Visibility.Hidden;
            txtPriceError.Text = "";

            if (cmbBoxSelectPrice.SelectedItem == null)
            {
                MessageBox.Show("Merci de sélectionner un tarif !", "Sélectionner un tarif !", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (!string.IsNullOrEmpty(txtBoxModNamePrice.Text) && !string.IsNullOrEmpty(txtBoxModValuePrice.Text))
                {
                    Prices priceSelect = cmbBoxSelectPrice.SelectedItem as Prices;

                    if (priceSelect != null)
                    {
                        int id = (int)priceSelect.Id;

                        if (priceService.AddUpdatePrice(priceSelect.Id, priceSelect))
                        {
                            txtPriceError.Foreground = new SolidColorBrush(Colors.Green);
                            txtPriceError.Visibility = Visibility.Visible;
                            txtPriceError.Text = "Tarif modifié !";

                            RefreshFrame();
                        }
                        else
                        {
                            LogTools.AddLog(LogTools.LogType.ERREUR, "Problème lors de la modification du prix");
                        }
                    }
                    else
                    {
                        LogTools.AddLog(LogTools.LogType.ERREUR, "Problème liste combo box vide !!");
                    }
                }
                else
                {
                    MessageBox.Show("Merci de remplir tous les champs pour la modification !", "Champs vides ! !", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }
        #endregion

        // Intéraction du bouton Supprimer un tarif et message de retour ////////////////////////////////////////////////////////////////////////////////////////////////
        #region Bouton Supprimer
        private void btnCustDel_Click(object sender, RoutedEventArgs e)
        {
            txtPriceError.Foreground = new SolidColorBrush(Colors.Red);
            txtPriceError.Visibility = Visibility.Hidden;
            txtPriceError.Text = "";

            if (cmbBoxSelectPrice.SelectedItem == null)
            {
                MessageBox.Show("Merci de sélectionner un tarif !", "Sélectionner un tarif !", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Prices priceSelect = cmbBoxSelectPrice.SelectedItem as Prices;

                if (priceSelect != null)
                {
                    var warning = MessageBox.Show($"Etes-vous sûr de vouloir supprimer le tarif : {priceSelect.Name} ?", "Suppression tarif", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                    if (warning == MessageBoxResult.OK)
                    {
                        int id = (int)priceSelect.Id;

                        priceService.DeletePrice(priceSelect);

                        txtPriceError.Foreground = new SolidColorBrush(Colors.Green);
                        txtPriceError.Visibility = Visibility.Visible;
                        txtPriceError.Text = "Tarif supprimé !";

                        RefreshFrame();
                    }
                }
                else
                {
                    LogTools.AddLog(LogTools.LogType.ERREUR, "Problème liste combo box vide !!");
                }
            }
        }
        #endregion


        //Actualise la frame de gauche
        #region RefreshFrame
        private void RefreshFrame()
        {
            var parent = Application.Current.MainWindow;
            Frame frameLeft = parent.FindName("FrameLeft") as Frame;
            frameLeft.Navigate(new InfoAjoutTarifsPage());
        }
        #endregion
    }
}

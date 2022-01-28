using Bredele_Gestion.viewmodel;
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
using System.Diagnostics;

namespace Bredele_Gestion
{
    /// <summary>
    /// Logique d'interaction pour AjoutReservationAdherentPage.xaml
    /// </summary>
    public partial class AjoutReservationAdherentPage : Page
    {
        ViewBookViewModel viewModel = new ViewBookViewModel();
        public AjoutReservationAdherentPage()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        #region
        private bool _flagTxtBoxNameReservation = true;
        private void TxtBoxNameReservation_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_flagTxtBoxNameReservation)
            {
                TxtBoxNameReservation.Text = "";
                _flagTxtBoxNameReservation = false;
            }
        }
        #endregion

        // Intéraction du bouton Valider une réservation ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Bouton Valider
        private void BtnValidReservation_Click(object sender, RoutedEventArgs e)
        {
            txtReservationError.Visibility = Visibility.Hidden;
            txtReservationError.Text = "";

            string error = viewModel.CheckBook();

            if (!string.IsNullOrEmpty(error))
            {
                txtReservationError.Visibility = Visibility.Visible;
                txtReservationError.Text = error;
            }
            else
            {
                viewModel.FormatToSendBookBdd();
            }
        }
        #endregion
    }
}

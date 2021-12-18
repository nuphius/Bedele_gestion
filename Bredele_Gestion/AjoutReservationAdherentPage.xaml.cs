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
    /// Logique d'interaction pour AjoutReservationAdherentPage.xaml
    /// </summary>
    public partial class AjoutReservationAdherentPage : Page
    {
        public AjoutReservationAdherentPage()
        {
            InitializeComponent();
        }

        private bool _flagTxtBoxNameReservation = true;

        private void TxtBoxNameReservation_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_flagTxtBoxNameReservation)
            {
                TxtBoxNameReservation.Text = "";
                _flagTxtBoxNameReservation = false;
            }
        }

        
    }
}

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

        private void TxtBoxNameReservation_MouseLeave(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(TxtBoxNameReservation.Text.ToString()))
            {
                TxtBoxNameReservation.Text = "Nom du réservant";
            }
        }
        private void TxtBoxNameReservation_MouseEnter(object sender, MouseEventArgs e)
        {
            TxtBoxNameReservation.Text = "";
        }
    }
}

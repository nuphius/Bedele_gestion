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
        }

        private void btnBoxSub_Click(object sender, RoutedEventArgs e)
        {
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
                txtBoxError.Visibility = Visibility.Visible;
            }
            
        }
    }
}

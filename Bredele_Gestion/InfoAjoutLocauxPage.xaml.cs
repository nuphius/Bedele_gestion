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
    /// Logique d'interaction pour InfoAjoutLocauxPage.xaml
    /// </summary>
    public partial class InfoAjoutLocauxPage : Page
    {
        ListLocauxService listLocaux = new ListLocauxService();
        public InfoAjoutLocauxPage()
        {
            InitializeComponent();
            listBoxAllBox.ItemsSource = listLocaux.ListLocaux;
        }

        // Intéraction du bouton Modifier qui renvoit vers la page de modification des locaux du local sélectionné dans la liste ////////////////////////////////////////
        #region Bouton Modifier
        private void btnModifyBox_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxAllBox.SelectedIndex != -1)
            {
                var boxSelected = listBoxAllBox.SelectedItems[0] as Locaux;

                if (boxSelected != null)
                {
                    if (int.TryParse(boxSelected.Id.ToString(), out int id))
                    {
                        var mainWindow = Application.Current.MainWindow;
                        var frame = mainWindow.FindName("FrameRight") as Frame;

                        if (frame != null)
                            frame.Navigate(new AjoutLocauxPage(id));
                    }
                }
                else
                    Console.WriteLine("Erreur sélection de la box dans la liste box est NULL !!");

            }
            else
            {
                MessageBox.Show("Sélectionnez une ligne a modifier.", "Aucune ligne séléctionnée", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
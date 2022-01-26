using BredeleGestion.Services;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Bredele_Gestion
{
    /// <summary>
    /// Logique d'interaction pour AdherentsPage.xaml
    /// </summary>
    public partial class AdherentsPage : Page
    {
        ListAdherentService cust = new ListAdherentService();
        public AdherentsPage()
        {
            InitializeComponent();
            this.DataContext = cust;

            //Valeur du filtre par défaut.
            cust.FilterSearchAdherent = 3;
            //affichage des customer dans la listView
            listBoxAdherentSearch.ItemsSource = cust.ListCust;
        }


        /// <summary>
        /// Fonction qui si un nom est séléctionné dans la listView, récupère la section et
        /// envoi l'Id de nom selectionné sur la page AjoutAdherentPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModifyAdherentSearch_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxAdherentSearch.SelectedIndex != -1)
            {
                var custSelected = listBoxAdherentSearch.SelectedItems[0] as ListAdherentService;

                if (int.TryParse(custSelected?.id.ToString(), out int id))
                {
                    var mainWindow = Application.Current.MainWindow;
                    var frame = mainWindow.FindName("FrameRight") as Frame;
                    frame?.Navigate(new AjoutAdherentPage(id));
                }
            }
            else
            {
                MessageBox.Show("Sélectionnez une ligne a modifier.", "Aucune ligne séléctionnée", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }


    /// <summary>
    /// Interface de convertion des boutons Radio
    /// </summary>
    public class RadioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value == int.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return parameter;
                else
                    return 0;
            }
            else
            {
                return 0;
            }
        }
    }
}
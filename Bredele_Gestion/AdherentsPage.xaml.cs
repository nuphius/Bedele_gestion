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
    /// Logique d'interaction pour AdherentsPage.xaml
    /// </summary>
    public partial class AdherentsPage : Page
    {
        ListAdherentService cust = new ListAdherentService();
        public AdherentsPage()
        {
            InitializeComponent();
            this.DataContext = cust;

            //Valeur du viltre par défaut.
            cust.FilterSearchAdherent = 3;
            //affichage des customer dans la listView
            listBoxAdherentSearch.ItemsSource = cust.ListCust;
        }

        private void rdButtonAdherentSearch_Checked(object sender, RoutedEventArgs e)
        {
            cust.FilterSearchAdherent = 1;
        }

        private void rdButtonTiersSearch_Checked(object sender, RoutedEventArgs e)
        {
            cust.FilterSearchAdherent = 2;
        }

        private void btnModifyAdherentSearch_Click(object sender, RoutedEventArgs e)
        {
            //FrameRight.Navigate(new AjoutAdherentPage());
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
            //return parameter;
            //throw new NotImplementedException();
        }
    }
}

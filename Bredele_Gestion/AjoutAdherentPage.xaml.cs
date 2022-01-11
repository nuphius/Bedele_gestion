using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    /// Logique d'interaction pour AjoutAdherentPage.xaml
    /// </summary>
    public partial class AjoutAdherentPage : Page
    {
        GestionAdherentsService adherentsService = new GestionAdherentsService();
        public AjoutAdherentPage()
        {
            InitializeComponent();
            this.DataContext = adherentsService;

            //Binding binding = new Binding("Text");
            //binding.Source = txtBoxCustFirstName;
            //lblCustError.SetBinding(Label.ContentProperty, binding);
        }

        private void btnCustSub_Click(object sender, RoutedEventArgs e)
        {
            string error = adherentsService.CheckInfos();
            if (!string.IsNullOrEmpty(error))
            {
                lblCustError.Visibility = Visibility.Visible;
                lblCustError.Content = error;
            }
        }

        private void txtBoxAddCP_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBoxAddCP.Text.Length == 5)
            {
                txtBoxAddCity.Text = adherentsService.SelectCity(adherentsService.Cp);
            }
        }

        private void txtBoxCustBirthDate_LostFocu(object sender, RoutedEventArgs e)
        {
            adherentsService.BirthDate = txtBoxCustBirthDate.Text;

            if (!adherentsService.birthDateFlag)
            {
                lblCustError.Visibility = Visibility.Visible;
                lblCustError.Content = "- Date au mauvais format ! (JJ/MM/AAAA)";
            }
            else
            {
                lblCustError.Visibility = Visibility.Hidden;
                lblCustError.Content = "";
            }
        }
    }
}

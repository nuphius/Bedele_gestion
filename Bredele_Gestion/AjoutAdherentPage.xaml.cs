using System;
using System.Collections.Generic;
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
        //private string _name;

        //public string Name
        //{
        //    get { return _name; }
        //    set { _name = value;
        //        Debug.WriteLine(value);
        //    }
        //}


        GestionAdherentsService adherentsService = new GestionAdherentsService() { Name = "Mik grosse bite"};
        public AjoutAdherentPage()
        {
            InitializeComponent();
            this.DataContext = adherentsService;
            //Binding binding = new Binding("Text");
            //binding.Source = txtBoxCustFirstName;
            //lblCustError.SetBinding(Label.ContentProperty, binding);
            Debug.WriteLine("----------------------------");
        }

        private void btnCustSub_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(adherentsService.Name);

            //if (true)
            //{

            //}

            //List<string> listForm = new List<string>();

            //string addUser = adherentsService.AddUser();

            //if (addUser != "")
            //{

            //}
            //else
            //{
            //    lblCustError.Visibility = Visibility.Visible;
            //    lblCustError.Content = "Ajout / Modification réalisé.";
            //}
        }
    }
}

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
    /// Logique d'interaction pour InfoAjoutTarifsPage.xaml
    /// </summary>
    public partial class InfoAjoutTarifsPage : Page
    {
        ListTarifService listPrice = new ListTarifService();
        public InfoAjoutTarifsPage()
        {
            InitializeComponent();
            listBoxAllPrice.ItemsSource = listPrice.ListPrice;
        }
    }
}
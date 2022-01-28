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
using Bredele_Gestion.viewmodel;

namespace Bredele_Gestion
{
    /// <summary>
    /// Logique d'interaction pour ViewBookPage.xaml
    /// </summary>
    public partial class ViewBookPage : Page
    {
        private int _idBox;
        public ViewBookPage(int idBox = 0)
        {
            _idBox = idBox;

            InitializeComponent();
            this.DataContext = new ViewListBookViewModel(_idBox);
        }
    }
}

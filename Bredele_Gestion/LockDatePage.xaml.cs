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
    /// Logique d'interaction pour LockDatePage.xaml
    /// </summary>
    public partial class LockDatePage : Page
    {
        public LockDatePage(int id)
        {
            InitializeComponent();
        }

        private void btnCalendar_Click(object sender, RoutedEventArgs e)
        {
            //var calendar = sender as Calendar;

            if (calendarLock.SelectedDate.HasValue)
            {
                DateTime date = calendarLock.SelectedDate.Value;

                var b = date.ToShortDateString();
                var a = date.ToString("yyyy_MM_dd");

                MessageBox.Show(b);
                MessageBox.Show(a);
            }
            
            
        }
    }
}

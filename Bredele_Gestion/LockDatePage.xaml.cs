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
    /// Logique d'interaction pour LockDatePage.xaml
    /// </summary>
    public partial class LockDatePage : Page
    {
        private int _idBox;

        public LockDatePage(int id)
        {
            InitializeComponent();
            _idBox = id;
        }

        private void btnCalendar_Click(object sender, RoutedEventArgs e)
        {
            if (calendarLock.SelectedDate.HasValue)
            {
                DateTime date = calendarLock.SelectedDate.Value;

                string dateFormat = date.ToString("yyyy-MM-dd");
                string requet = string.Format(RequetSqlService.UPDATEDATEBOX, dateFormat, _idBox);

                ConnexionBddService connexionBddService = new ConnexionBddService(requet, RequetSqlService.TABLEBOX);

                MessageBoxResult warning = MessageBox.Show("Etes-vous sûr vouloir verrouiller la loge jusqu'a " + date.ToShortDateString(), "Confirmation verrouillage", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (warning == MessageBoxResult.OK)
                {
                    if (!connexionBddService.InsertRequet())
                        LogTools.AddLog(LogTools.LogType.ERREUR, "Problème lors de l'insertion de la date verrouillage dans la BDD");
                    else
                        MessageBox.Show("La Loge a bien été verrouillée.", "Confirmation Verrouillage", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                var mainWindow = Application.Current.MainWindow;
                Frame frameRight = mainWindow.FindName("FrameRight") as Frame;
                Frame frameLeft = mainWindow.FindName("FrameLeft") as Frame;
                frameLeft.Navigate(new AdherentsPage());
                frameRight.Navigate(new ViewBoxPage());

            }
            
            
        }
    }
}

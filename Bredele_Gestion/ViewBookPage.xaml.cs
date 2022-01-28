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
using BredeleGestion.Services;

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

        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (listViewBook.SelectedItem != null)
            {
                BookBox book = listViewBook.SelectedItem as BookBox;

                MessageBoxResult warning = MessageBox.Show($"Confirmer la suppression de la réservation de {book.NameCust} {book.FirstNameCust} ?", 
                    "Confirmation de suppression", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

                if (warning == MessageBoxResult.OK)
                {
                    ViewBookService viewBookService = new ViewBookService();

                    bool delete = viewBookService.DeleteBook(book.IdBook);

                    if (delete)
                    {
                        MessageBox.Show("Réservation supprimée", "Confirmation de suppression", MessageBoxButton.OK, MessageBoxImage.Information);

                        var mainWindow = Application.Current.MainWindow;
                        Frame frameRight = mainWindow.FindName("FrameRight") as Frame;
                        frameRight.Navigate(new ViewBookPage(_idBox));
                    }
                    else
                    {
                        LogTools.AddLog(LogTools.LogType.ERREUR, "Problème lors de la suppression d'une réservation");
                    }
                }
            }
        }
    }
}

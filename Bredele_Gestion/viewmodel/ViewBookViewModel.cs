using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BredeleGestion.Services;

namespace Bredele_Gestion.viewmodel
{


    public class ViewBookViewModel : INotifyPropertyChanged
    {
        ListAdherentService adherentService = new ListAdherentService();
        GestionBookService bookService = new GestionBookService();
        GestionTarifsService gestionPrices = new GestionTarifsService();

        public ObservableCollection<Prices> ListPrice { get; set; }
        public List<Activitys> ListActivity { get; set; }
        public ObservableCollection<ListAdherentService> ListCust { get; set; }
        //public ListAdherentService SelectCust { get; set; }
        public BoxPlace SelectPlace { get; set; }
        readonly Regex regex = new Regex("^[0-9]{0,2}$");
        public DateTime SelectedDate { get; set; }

        private Activitys _selectActivity;
        private ObservableCollection<BoxPlace> _listBox;
        private string _searchName;
        private string _hoursEnd;
        private string _hoursStart;
        private string _price;
        private Prices _selectedPrice;
        private ListAdherentService _selectCust;
        private string _adherent;




        public string Adherent
        {
            get { return _adherent; }
            set 
            { 
                _adherent = value;
                this.NotifyPropertyChanged(nameof(Adherent));
            }
        }


        public ListAdherentService SelectCust
        {
            get { return _selectCust; }
            set
            {
                _selectCust = value;

                if (SelectCust.Adherent)
                {
                    Adherent = "Adhérent";
                }
                else
                {
                    Adherent = "Non Adhérent !";
                }
            }
        }


        public Prices SelectedPrice
        {
            get { return _selectedPrice; }
            set
            {
                _selectedPrice = value;
                CalculPrice();
            }
        }

        public Activitys SelectActivity
        {
            get { return _selectActivity; }
            set
            {
                _selectActivity = value;
                ListBox = bookService.LoadBoxs(SelectActivity.Id, true);

                var mainWindow = Application.Current.MainWindow;
                Frame frameRight = mainWindow.FindName("FrameRight") as Frame;
                frameRight.Navigate(new ViewBoxPage(SelectActivity.Id));

                this.NotifyPropertyChanged(nameof(SelectActivity));
            }
        }

        public ObservableCollection<BoxPlace> ListBox
        {
            get { return _listBox; }
            set
            {
                _listBox = value;
                this.NotifyPropertyChanged(nameof(ListBox));
            }
        }

        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;

                this.NotifyPropertyChanged(nameof(Price));
            }
        }

        #region SearchName
        /// <summary>
        /// proprieté de recherche de nom
        /// </summary>
        public string SearchName
        {
            get { return _searchName; }
            set
            {
                _searchName = value;
                adherentService.SelectCustomers(value);
                ListCust = adherentService.ListCust;
            }
        }
        #endregion

        #region HoursStart
        /// <summary>
        /// proprieté bindé pour l'heure de début
        /// </summary>
        public string HoursStart
        {
            get { return _hoursStart; }
            set
            {
                if (value != "")
                {
                    if (regex.IsMatch(value) && int.Parse(value) < 25)
                    {
                        _hoursStart = value;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_hoursStart) && !string.IsNullOrEmpty(value))
                        {
                            value = HoursStart;
                        }
                        else
                        {
                            _hoursStart = string.Empty;
                            value = string.Empty;
                        }
                    }
                }
                else
                {
                    _hoursStart = string.Empty;
                    value = string.Empty;
                }

                this.NotifyPropertyChanged(nameof(HoursStart));
            }
        }
        #endregion

        #region HoursEnd
        /// <summary>
        /// propriété bindé pour l'heure de fin
        /// </summary>
        public string HoursEnd
        {
            get { return _hoursEnd; }
            set
            {
                if (value != "")
                {
                    if (regex.IsMatch(value) && int.Parse(value) < 25)
                    {
                        _hoursEnd = value;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(_hoursEnd) && !string.IsNullOrEmpty(value))
                        {
                            value = HoursEnd;
                        }
                        else
                        {
                            _hoursEnd = string.Empty;
                            value = string.Empty;
                        }
                    }
                }
                else
                {
                    _hoursEnd = string.Empty;
                    value = string.Empty;
                }

                this.NotifyPropertyChanged(nameof(HoursEnd));
            }
        }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        public ViewBookViewModel()
        {
            SearchName = "";
            ListActivity = bookService.LoadActivity();

            gestionPrices.LoadListPrice();
            ListPrice = gestionPrices.PriceSelected;
        }

        #region CheckBook
        /// <summary>
        /// Vérifier la conformité des champs du formulaire
        /// </summary>
        /// <returns></returns>
        public string CheckBook()
        {
            try
            {
                string error = string.Empty;
                int hourS = int.Parse(HoursStart);
                int hourE = int.Parse(HoursEnd);

                if (SelectCust == null)
                    error += "- Vous n'avez sélectionné aucun nom !\n";
                if (SelectedDate.ToShortDateString() == "01/01/0001")
                    error += "- Vous n'avez sélectionné aucune date !\n";
                if (SelectedDate.Date < DateTime.Now.Date)
                    error += "- Date antérieur à aujourd'hui " + DateTime.Now.ToShortDateString() + " !\n";
                if (string.IsNullOrEmpty(HoursStart))
                    error += "- Vous n'avez pas saisie d'heure de début !\n";
                if (string.IsNullOrEmpty(HoursEnd))
                    error += "- Vous n'avez pas saisie d'heure de fin !\n";
                if (hourE <= hourS)
                    error += "- Heure de fin inférieur ou égal à l'heure de début !\n";
                if (SelectActivity == null)
                    error += "- Vous n'avez sélectionné aucune activité !\n";
                if (SelectPlace == null)
                    error += "- Vous n'avez sélectionné aucune capacité de place !\n";

                return error;
            }
            catch (Exception ex)
            {
                LogTools.AddLog(LogTools.LogType.ERREUR,"Problème lors du remplissage d'une réservation" + ex.Message);
                return "Error voir log";
            }
            
        }
        #endregion

        #region FormatToSendBookBdd
        /// <summary>
        /// Met au bon format les données du formulaire et envoi a la BDD
        /// </summary>
        public bool FormatToSendBookBdd()
        {
            try
            {
                string hourStart = HoursStart.Length == 1 ? $"0{HoursStart}:00:00" : $"{HoursStart}:00:00";
                string hoursEnd = HoursEnd.Length == 1 ? $"0{HoursEnd}:00:00" : $"{HoursEnd}:00:00";


                int custId = SelectCust.id;
                string date = SelectedDate.ToString("yyyy-MM-dd");

                int activity = SelectActivity.Id;
                int idBox = SelectPlace.Id;

                if (!bookService.SendBookBdd(custId, date, hourStart, hoursEnd, idBox))
                {
                    LogTools.AddLog(LogTools.LogType.ERREUR, "Problème d'insertion des donnée dans la BDD Book");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                LogTools.AddLog(LogTools.LogType.ERREUR, "Problème de convertion des variables (réservation) avant envoi à la BDD Book");
                return false;
            }
        }
        #endregion

        private void CalculPrice()
        {
            if (!String.IsNullOrEmpty(HoursStart) && !String.IsNullOrEmpty(HoursEnd) && SelectActivity != null)
            {
                try
                {
                    int.TryParse(HoursStart, out int hStart);
                    int.TryParse(HoursEnd, out int hEnd);
                    int nbHours = hEnd - hStart;

                    decimal price = (_selectedPrice.Price * nbHours) * SelectActivity.PriceAtivity;

                    Price = String.Format("{0:0.##}", price) + " €";
                }
                catch (Exception ex)
                {
                    LogTools.AddLog(LogTools.LogType.ERREUR, "Problème convertion des heures pour calcul tarif" + ex.Message);
                }

            }
        }

        #region NotifyPropertyChanged
        /// <summary>
        /// Fonction signalent le changement de valeur dans l'evenelent rattaché
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}

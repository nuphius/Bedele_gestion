using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BredeleGestion.Services;

namespace Bredele_Gestion.viewmodel
{


    public class ViewBookViewModel : INotifyPropertyChanged
    {
        ListAdherentService adherentService = new ListAdherentService();
        public ObservableCollection<ListAdherentService> ListCust { get; set; }

        readonly Regex regex = new Regex("^[0-9]{0,2}$");

        public DateTime SelectedDate { get; set; }

        private string _searchName;
        private string _hoursEnd;
        private string _hoursStart;
        public List<BoxPlace> ListPlaces { get; set; }
        public List<Activity> ListActivity { get; set; }

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
            GestionBookService bookService = new GestionBookService();
            SearchName = "";
            ListActivity = bookService.LoadActivity();
            ListPlaces = bookService.LoadPlace();
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

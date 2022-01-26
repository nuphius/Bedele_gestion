using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BredeleGestion.Services;

namespace Bredele_Gestion.viewmodel
{


    public class ViewBookViewModel : INotifyPropertyChanged
    {
        ListAdherentService adherentService = new ListAdherentService();
        public ObservableCollection<ListAdherentService> ListCust { get; set; }

        private string _searchName;

        public string SearchName
        {
            get { return _searchName; }
            set 
            { 
                _searchName = value;
                adherentService.SelectCustomers(value);
                ListCust = adherentService.ListCust;
                //this.NotifyPropertyChanged(nameof(SearchName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ViewBookViewModel()
        {
            SearchName = "";
            //adherentService.SelectCustomers();
            //ListCust = adherentService.ListCust;
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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace BredeleGestion.Services
{
    public class ListAdherentService : INotifyPropertyChanged
    {
        //Affichage de la liste des adhérents /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        #region Déclaration des prorpiétés

        public int id;
        public string Name;
        public bool Adherent;
        private string _fisrtname;
        private bool _adherent;
        private ObservableCollection<ListAdherentService> _listCust = new ObservableCollection<ListAdherentService>();

        private string _nameSearch;

        private int _filterSearchAdherent;
        private string _nbAdherents;
        private string _nbTiers;

        public ObservableCollection<ListAdherentService> ListCust
        {
            get { return _listCust; }
            set { _listCust = value; }
        }

        public int FilterSearchAdherent
        {
            get { return _filterSearchAdherent; }
            set
            {
                _filterSearchAdherent = value;
                this.NotifyPropertyChanged(nameof(FilterSearchAdherent));

                SelectCustomers();
            }
        }

        public string NbAdherents
        {
            get { return _nbAdherents; }
            set
            {
                _nbAdherents = value;
                this.NotifyPropertyChanged(nameof(_nbAdherents));
            }
        }
        public string NbTiers
        {
            get { return _nbTiers; }
            set
            {
                _nbTiers = value;
                this.NotifyPropertyChanged(nameof(_nbTiers));
            }
        }
        public string NameSearch
        {
            get { return _nameSearch; }
            set
            {
                _nameSearch = value;
                SelectCustomers(_nameSearch);
                this.NotifyPropertyChanged(nameof(_nameSearch));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public ListAdherentService(int id = 0, string name = "", string firstname = "", bool adherent = false)
        {
            this.id = id;
            Name = name ?? string.Empty;
            Adherent = adherent;
            _fisrtname = firstname ?? string.Empty;
            _adherent = adherent;

            NbAdherents = CountTypeCust(RequetSqlService.COUNTADHERENT, "adhérents");
            NbTiers = CountTypeCust(RequetSqlService.COUNTNOADHERENT, "tiers");
        }

        #region SelectCustomers
        /// <summary>
        /// Récupère dans la BDD la liste des customers et crée une list observable
        /// gère égalemement la recherche par nom
        /// </summary>
        /// <param name="nameSearch"></param>
        public void SelectCustomers( string nameSearch="")
        {
            if (_listCust.Count > 0)
            {
                _listCust.Clear();
            }

            string where = " WHERE custadherent=";

            if (!string.IsNullOrEmpty(nameSearch))
            {
                where = $" WHERE custname LIKE '%{nameSearch}%'";
            }
            else
            {
                switch (FilterSearchAdherent)
                {
                    case 1:
                        where += $"'{true}'";
                        break;
                    case 2:
                        where += $"'{false}'";
                        break;
                    default:
                        where = "";
                        break;
                }
            }

            string requetSql = RequetSqlService.SELECTCUSTSEARCH + where + " ORDER BY custname";

            ConnexionBddService connexionBddService = new ConnexionBddService(requetSql, RequetSqlService.TABLECUST);
            List<DataRow> listCust = connexionBddService.ExecuteRequet();

            try
            {
                if (listCust != null)
                {
                    foreach (var cust in listCust)
                    {
                        _listCust.Add(new ListAdherentService((int)cust["custid"],
                            cust["custname"].ToString(), cust["custfirstname"].ToString(), (bool)cust["custadherent"]));
                    }
                }
            }
            catch (System.Exception ex)
            {
                LogTools.AddLog(LogTools.LogType.ERREUR, "Erreur récupération customer par filtre adherent" + ex.Message);
            }
        }
        #endregion


        #region CountTypeCust
        /// <summary>
        /// Compte le nombre d'adhérent et tiers
        /// </summary>
        /// <param name="requet"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string CountTypeCust(string requet, string type)
        {
            ConnexionBddService connexionBddService = new ConnexionBddService(requet, RequetSqlService.TABLECUST);
            List<DataRow> nbCust = connexionBddService.ExecuteRequet();

            return $"Actuellement {nbCust[0]["nb"]} {type}";
        }
        #endregion

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

        #region Override de ToString()
        /// <summary>
        /// Surcharge de la fonction ToString de l'instance pour la listview
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //return _name.ToUpper() + " " + _fisrtname + " " + (_adherent ? "Adhérent" : "");
            return Name.ToUpper() + " " + _fisrtname;
        }
        #endregion
    }
}
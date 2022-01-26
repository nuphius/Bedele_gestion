using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BredeleGestion.Services
{
    public class GestionTarifsService : INotifyPropertyChanged
    {
        //private double _newPriceDouble;

        private string _nameNewPrice;
        private string _newPrice;
        private ObservableCollection<Prices> _priceSelected = new ObservableCollection<Prices>();
        private string _updateName;
        private string _updateValue;

        #region Liste des propriétés
        public string UpdateValue
        {
            get { return _updateValue; }
            set
            {
                Regex regex = new Regex("^[0-9]+\\.?,?[0-9]{0,2}$");

                if (regex.IsMatch(value.ToString()))
                {
                    _updateValue = value.Replace(",", ".");
                }
                else
                {
                    if (!string.IsNullOrEmpty(_updateValue) && !string.IsNullOrEmpty(value))
                    {
                        value = _updateValue;
                    }
                    else
                    {
                        _updateValue = string.Empty;
                        value = string.Empty;
                    }
                }
                NotifyPropertyChanged(nameof(UpdateValue));
            }
        }

        public string UpdateName
        {
            get { return _updateName; }
            set { _updateName = value; }
        }


        public ObservableCollection<Prices> PriceSelected
        {
            get { return _priceSelected; }
            set
            {
                _priceSelected = value;
                this.NotifyPropertyChanged(nameof(PriceSelected));
            }
        }

        public string NameNewPrice
        {
            get { return _nameNewPrice; }
            set
            {
                _nameNewPrice = value;
                this.NotifyPropertyChanged(nameof(NameNewPrice));
            }
        }

        #region Proprieté NewPrice
        /// <summary>
        /// proprieté NewPrice qui filtre les saisies afin de n'avoir que le bon format
        /// </summary>
        public string NewPrice
        {
            get { return _newPrice; }
            set
            {
                Regex regex = new Regex("^[0-9]+\\.?,?[0-9]{0,2}$");

                if (regex.IsMatch(value.ToString()))
                {
                    _newPrice = value.Replace(",", ".");
                }
                else
                {
                    if (!string.IsNullOrEmpty(_newPrice) && !string.IsNullOrEmpty(value))
                    {
                        value = _newPrice;
                    }
                    else
                    {
                        _newPrice = string.Empty;
                        value = string.Empty;
                    }
                }
                NotifyPropertyChanged(nameof(NewPrice));
            }
        }
        #endregion
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region NotifyPropertyChanged
        /// <summary>
        /// Fonction pour actualiser SI changement de valeur d'une propriété.
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region LoadListPrice
        /// <summary>
        /// Chargement de tous les tarifs dans le menu déroulant tarif
        /// </summary>
        public void LoadListPrice()
        {
            try
            {
                //List<Prices> listePrice = new List<Prices>();

                ConnexionBddService connexionBddService = new ConnexionBddService(RequetSqlService.SELECTALLPRICE, RequetSqlService.TABLEPRICE);
                List<DataRow> ListBddPrice = connexionBddService.ExecuteRequet();

                foreach (DataRow row in ListBddPrice)
                {
                    PriceSelected.Add(new Prices { Id = (int)row["priceid"], Name = row["pricename"].ToString(), Price = (decimal)row["pricevalue"] });
                }

                //PriceSelected = listePrice;

            }
            catch (Exception ex)
            {
                LogTools.AddLog(LogTools.LogType.ERREUR, "Problème de récupération de la table Price" + ex.Message);
                //return null;
            }
        }
        #endregion

        #region AddUpdatePrice
        /// <summary>
        /// Ajouter ou modifie dans la BDD le tarif séléctionné
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AddUpdatePrice(int id = 0, Prices price = null)
        {
            string requetPrice;

            if (id == 0)
            {
                requetPrice = string.Format(RequetSqlService.ADDPRICE, _nameNewPrice, _newPrice);
            }
            else
            {
                requetPrice = string.Format(RequetSqlService.UPDATEPRICE, _updateName, _updateValue, id);

                //_priceSelected.Where(x => x.Id == id).Select(y => y.Price = decimal.Parse(_updateValue));

                PriceSelected.Remove(price);
                PriceSelected.Add(new Prices { Id = (int)price.Id, Name = _updateName, Price = decimal.Parse(_updateValue.Replace(".", ",")) });
            }

            ConnexionBddService connexionBddService = new ConnexionBddService(requetPrice, RequetSqlService.TABLEPRICE);

            bool flag = connexionBddService.InsertRequet();

            if (id == 0)
            {
                connexionBddService = new ConnexionBddService(RequetSqlService.LASTINSERT, RequetSqlService.TABLEPRICE);
                List<DataRow> lastBdd = connexionBddService.ExecuteRequet();

                PriceSelected.Add(new Prices { Id = (int)lastBdd[0]["priceid"], Name = _nameNewPrice, Price = decimal.Parse(_newPrice.Replace(".", ",")) });
            }

            return flag;
        }
        #endregion

        #region DeletePrice
        /// <summary>
        /// Supprime le tarif passé en parametre
        /// </summary>
        /// <param name="id"></param>
        public void DeletePrice(Prices price)
        {
            ConnexionBddService connexionBddService = new ConnexionBddService(string.Format(RequetSqlService.DELETEPRICE, price.Id), RequetSqlService.TABLEPRICE);

            connexionBddService.InsertRequet();

            PriceSelected.Remove(price);
        }
        #endregion
    }

    public class Prices
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public override string ToString()
        {
            return Name.ToString() + " " + Price.ToString() + "€";
        }
    }
}

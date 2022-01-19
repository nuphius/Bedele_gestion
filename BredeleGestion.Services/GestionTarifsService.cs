using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace BredeleGestion.Services
{
    public class GestionTarifsService : INotifyPropertyChanged
    {
        private double _newPriceDouble;

        private string _nameNewPrice;
        private string _newPrice;

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
                Regex regex = new Regex("^[0-9]+\\.?,?[0-9]*$");

                if (regex.IsMatch(value.ToString()))
                {
                    string checkNb = value.Replace(".", ",");

                    if (Double.TryParse(checkNb, out _newPriceDouble))
                    {
                        _newPrice = checkNb;
                    }
                    else
                        LogTools.AddLog(LogTools.LogType.ERREUR, "Erreur - Convertion de l'ajout d'un nouveau prix en double");
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

        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Fonction pour actualiser SI changement de valeur d'une propriété.
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public bool AddUpdatePrice(int id)
        {
            //string requetprice;


            //if (id == 0)
            //{
            //    requetPrice = RequetSqlService.ADDCUST +
            //    ($"('{civility}', '{_name}', '{_firstName}', '{_phone}', '{_mail}', '{dateConverted}', '{adherent}', '{_idCity}', '{_address}', '{_address2}')");
            //}
            //else
            //{

            //    //requetCust = string.Format(RequetSqlService.UPDATECUST, $"'{civility}'", $"'{_name}'", $"'{_firstName}'", $"'{_phone}'", $"'{_mail}'",
            //    //    $"'{dateConverted}'", $"'{adherent}'", $"'{_idCity}'", $"'{_address}'", $"'{_address2}'", $"'{id}'");
            //}

            //ConnexionBddService connexionBddService = new ConnexionBddService(requetCust, RequetSqlService.TABLECUST);

            //return connexionBddService.InsertRequet();

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;

namespace BredeleGestion.Services
{
    public partial class GestionAdherentsService : INotifyPropertyChanged
    {
        public bool birthDateFlag = false;
        public bool mailFlag = false;

        private string _errorMessage = string.Empty;
        private int _idCust = 0;
        private string _name;
        private string _firstName;
        private string _address;
        private string _cp;
        private int _idCity;
        private string _city;
        private string _birthDate;
        private string _phone;
        private string _mail;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Déclaration des Proprietés
        public string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value.Trim();

                }
                else
                {
                    _name = string.Empty;
                }
                this.NotifyPropertyChanged(nameof(Name));
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _firstName = value.Trim();
                }
                else
                {
                    _firstName = string.Empty;
                }
                this.NotifyPropertyChanged(nameof(FirstName));
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _address = value.Trim();
                }
                else
                {
                    _address = string.Empty;
                }
                this.NotifyPropertyChanged(nameof(Address));
            }
        }

        /// <summary>
        /// Contrôle que seul des chiffres sont saisies pour le code postal
        /// </summary>
        public string Cp
        {
            get { return _cp; }
            set
            {
                Regex regex = new Regex(@"^[0-9]*[0-9]$");

                if (value != null)
                {
                    if (value.Length > 0)
                    {
                        if (regex.IsMatch(value))
                        {   
                            _cp = value;

                            if (_cp.Length == 5)
                            {
                                City = SelectCity(_cp);
                            }
                            else
                            {
                                City = string.Empty;
                            }
                        }
                        else
                        {
                            Cp = _cp;
                        }
                    }
                    else if (value.Length == 0 && _cp.Length == 1)
                    {
                        _cp = "";
                        Cp = "";
                    }
                }
                this.NotifyPropertyChanged(nameof(Cp));
            }
        }

        #region Gestion par un ContextData de la ville
        /// <summary>
        /// Gestion par un ContextData de la ville
        /// </summary>
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                this.NotifyPropertyChanged(nameof(City));
            }
        }
        #endregion

        /// <summary>
        /// Contrôle que la date de naissance est bien saisie au format jj/mm/aaaa
        /// </summary>
        public string BirthDate
        {
            get { return _birthDate; }
            set
            {
                Regex regex = new Regex(@"^[0-9]{2}/{1}[0-9]{2}/{1}[0-9]{4}$");

                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Length > 0)
                    {
                        if (regex.IsMatch(value))
                        {
                            _birthDate = value;
                            birthDateFlag = true;
                        }
                        else
                        {
                            BirthDate = _birthDate;
                            birthDateFlag = false;
                        }
                    }
                    else if (value.Length == 0 && _birthDate.Length == 1)
                    {
                        _birthDate = "";
                        BirthDate = "";

                    }
                }
                this.NotifyPropertyChanged(nameof(BirthDate));
            }
        }

        /// <summary>
        /// Contrôle que seul des chiffres sont saisies pour le téléphone
        /// </summary>
        public string Phone
        {
            get { return _phone; }
            set
            {
                Regex regex = new Regex(@"^[0-9]*[0-9]$");

                if (value != null)
                {
                    if (value.Length > 0)
                    {
                        if (regex.IsMatch(value))
                        {
                            _phone = value;
                        }
                        else
                        {
                            Phone = _phone;
                        }
                    }
                    else if (value.Length == 0 && _phone.Length == 1)
                    {
                        _phone = "";
                        Phone = "";
                    }
                }
                this.NotifyPropertyChanged(nameof(Phone));
            }
        }

        public string Mail
        {
            get { return _mail; }
            set 
            {
                Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                
                if (value != null)
                {   
                    if (regex.IsMatch(value))
                    {
                        _mail = value;
                        mailFlag = true;

                    }
                    else
                    {
                        _mail = value;
                        mailFlag = false;
                    }
                }
                this.NotifyPropertyChanged(nameof(Mail));
            }
        }
        #endregion

        public GestionAdherentsService(int idCust = 0)
        {
            _idCust = idCust;
        }


        #region Vérification des champs si vide
        /// <summary>
        /// Vérification que les champs obligatoire du formulaire ne sont pas vide
        /// Renvoi une chaine de caractère avec les eventuelles erreurs
        /// </summary>
        /// <returns></returns>
        public string CheckInfos()
        {
            _errorMessage = string.Empty;

            if (string.IsNullOrEmpty(_name))
            {
                _errorMessage += "- Le champ NOM est obligatoire\n";
            }
            if (string.IsNullOrEmpty(_firstName))
            {
                _errorMessage += "- Le champ PRENON est obligatoire\n";
            }
            if (string.IsNullOrEmpty(_address))
            {
                _errorMessage += "- Le champ ADRESSE est obligatoire\n";
            }
            if (string.IsNullOrEmpty(_cp))
            {
                _errorMessage += "- Le champ CODE POSTAL est obligatoire\n";
            }
            if (string.IsNullOrEmpty(_city))
            {
                _errorMessage += "- Le champ VILLE est obligatoire\n";
            }
            if (string.IsNullOrEmpty(_birthDate))
            {
                _errorMessage += "- Le champ DATE DE NAISSANCE est obligatoire\n";
            }
            if (string.IsNullOrEmpty(_phone))
            {
                _errorMessage += "- Le champ TELEPHONE est obligatoire\n";
            }
            if (string.IsNullOrEmpty(_mail))
            {
                _errorMessage += "- Le champ MAIL est obligatoire\n";
            }
            return _errorMessage;
        }
        #endregion

        public string SelectCity(string cp)
        {
            ConnexionBddService connexionBddService = new ConnexionBddService(RequetSqlService.SELECTCPCITY + cp, RequetSqlService.TABLECITY);

            List<DataRow> listCity = connexionBddService.ExecuteRequet();

            foreach (var item in listCity)
            {
                City = item["addcity"].ToString();

                try
                {
                    _idCity = (int)item["addid"];
                }
                catch (Exception ex)
                {
                    LogTools.AddLog(LogTools.LogType.ERREUR, "Erreur - impossible de convertir id de la table city en INT" + ex);
                }
                
            }
            return City;
        }

        public bool AddUser(bool adherent, string civility)
        {
            string requetCust;

            if (_idCust == 0)
            {
                requetCust = RequetSqlService.ADDCUST +
                ($"('{civility}', '{_name}', '{_firstName}', '{_phone}', '{_mail}', '{_birthDate}', '{adherent}', '{_idCity}')");
            }
            else
            {
                requetCust = "";
            }

            ConnexionBddService connexionBddService = new ConnexionBddService(requetCust, RequetSqlService.TABLECUST);

            return connexionBddService.InsertRequet();
        }

        public void DeleteUser()
        {

        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}

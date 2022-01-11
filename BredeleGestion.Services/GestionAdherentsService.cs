﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;

namespace BredeleGestion.Services
{
    public partial class GestionAdherentsService : INotifyPropertyChanged
    {
        private string _errorMessage = string.Empty;
        private string _name;
        private string _firstName;
        private string _address;
        private string _cp;
        private string _city;
        private string _birthDate;
        public bool birthDateFlag = false;
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
                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
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
                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(FirstName)));
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
                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
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

                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Cp)));
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                _city = value;

                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(City)));
            }
        }

        /// <summary>
        /// Contrôle que la date de naiossance est bien saisie au format jj/mm/aaaa
        /// </summary>
        public string BirthDate
        {
            get { return _birthDate; }
            set
            {
                Regex regex = new Regex(@"^[0-9]{2}/{1}[0-9]{2}/{1}[0-9]{4}$");

                if (value != null)
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

                if (PropertyChanged != null)
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(BirthDate)));
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
                    }

                }
                
                _mail = value; 
            }
        }
        #endregion

        public GestionAdherentsService()
        {
            //Name = _name;
        }



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

        public string SelectCity(string cp)
        {
            ConnexionBddService connexionBddService = new ConnexionBddService(RequetSqlService.SELECTCPCITY + cp, RequetSqlService.TABLECITY);

            List<DataRow> listCity = connexionBddService.ExecuteRequet();

            foreach (var item in listCity)
            {
                City = item["addcity"].ToString();
            }
            return City;
        }

        public string AddUser()
        {
            return "";
        }

        public void DeleteUser()
        {

        }
    }
}

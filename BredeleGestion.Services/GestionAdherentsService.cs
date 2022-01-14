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

        private int _civility;
        private string _errorMessage = string.Empty;
        private int _idCust = 0;
        private string _name;
        private string _firstName;
        private string _address;
        private string _address2;
        private string _cp;
        private int _idCity;
        private string _city;
        private string _birthDate;
        private string _phone;
        private string _mail;
        private bool _adherent;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Déclaration des Proprietés

        public int Civility
        {
            get { return _civility; }
            set
            {
                _civility = value;
                this.NotifyPropertyChanged(nameof(Civility));
            }
        }


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

        public string Address2
        {
            get { return _address2; }
            set
            {
                _address2 = value;
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

        public bool Adherent
        {
            get { return _adherent; }
            set
            {
                _adherent = value;

                this.NotifyPropertyChanged(nameof(Adherent));
            }
        }

        #endregion

        public GestionAdherentsService()
        {
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

        public void LoadUser(int idUser)
        {
            ConnexionBddService connexionBddService = new ConnexionBddService(RequetSqlService.SELECTUSER + idUser, RequetSqlService.TABLECUST);
            List<DataRow> listUsers = connexionBddService.ExecuteRequet();

            foreach (var user in listUsers)
            {
                Name = user["custname"].ToString();
                FirstName = user["custfirstname"].ToString();
                Address = user["custaddress"].ToString();
                Address2 = user["custaddress2"].ToString();
                Cp = user["addpostal"].ToString();
                BirthDate = String.Format("{0:dd/MM/yyyy}", user["custbirthdate"]);
                Phone = user["custphone"].ToString();
                Mail = user["custmail"].ToString();


                if (user["custadherent"].Equals(true))
                {
                    Adherent = true;
                }
                else
                {
                    Adherent = false;
                }

                switch (user["custcivility"].ToString())
                {
                    case "Mlle":
                        Civility = 1;
                        break;
                    case "Mme":
                        Civility = 0;
                        break;
                    case "M":
                        Civility = 2;
                        break;
                    default:
                        break;
                }
            }
        }

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

        public bool AddUpdateUser(bool adherent, string civility,int id)
        {
            string requetCust;
            string dateConverted = ConvertDate();


            if (id == 0)
            {
                requetCust = RequetSqlService.ADDCUST +
                ($"('{civility}', '{_name}', '{_firstName}', '{_phone}', '{_mail}', '{dateConverted}', '{adherent}', '{_idCity}', '{_address}', '{_address2}')");
            }
            else
            {
                
                requetCust = string.Format(RequetSqlService.UPDATECUST, $"'{civility}'", $"'{_name}'", $"'{_firstName}'", $"'{_phone}'", $"'{_mail}'",
                    $"'{dateConverted}'", $"'{adherent}'", $"'{_idCity}'", $"'{_address}'", $"'{_address2}'", $"'{id}'");
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

        private string ConvertDate()
        {
            string[] tabConvertDate = _birthDate.Split('/');
            return $"{tabConvertDate[2]}-{tabConvertDate[1]}-{tabConvertDate[0]}";
        }
    }
}

using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace BredeleGestion.Services
{
    public partial class GestionAdherentsService
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

                            //if (_cp.Length == 5)
                            //{
                            //    SelectCity(_cp);
                            //}
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
            }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }


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
            set { _mail = value; }
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

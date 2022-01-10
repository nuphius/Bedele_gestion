using System;
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
        private string _birthDate;
        public string Name
        {
            get { return _name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    _errorMessage += " - Le champ NOM est obligatoir\n";
                }
                else
                {
                    _name = value.Trim();
                }
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _errorMessage += " - Le champ PRENON est obligatoir\n";
                }
                else
                {
                    _firstName = value.Trim();
                }
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _errorMessage += " - Le champ ADRESSE est obligatoir\n";
                }
                else
                {
                    _address = value.Trim();
                }
            }
        }

        public string Cp
        {
            get { return _cp; }
            set
            {
                Regex regex = new Regex("[^0-9]+");


                if (string.IsNullOrWhiteSpace(value))
                {
                    _errorMessage += " - Le champ CODE POSTAL est obligatoir\n";
                }
                //else if (value.Length != 5)
                //{
                //    _errorMessage += " - Entrez un CODE POSTAL Valide ! (5 chiffres)\n";
                //}
                else if (regex.IsMatch(value))
                {
                    _cp = value;
                }
                else
                {
                    Name = _cp;
                }
            }
        }

        public string BirthDate
        {
            get { return _birthDate; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _errorMessage += " - Le champ DATE DE NAISSANCE est obligatoir\n";
                }
                else
                {
                    _birthDate = value.Trim();
                }
            }
        }

        private string _phone;

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (string.IsNullOrWhiteSpace(_phone))
                {
                    _errorMessage += " - Le champ THELEPHONE est obligatoir\n";
                }
                else if (_phone.Length != 10)
                {
                    _errorMessage += "Entrer un TELEPHONE valide (10 chiffres)";
                }
                else
                {
                    _phone = value.Trim();
                }
            }
        }



        public GestionAdherentsService()
        {
            Name = _name;
        }




        public void CheckInfos()
        {

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

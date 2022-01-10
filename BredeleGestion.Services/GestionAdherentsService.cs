using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace BredeleGestion.Services
{
    public partial class GestionAdherentsService
    {
        public GestionAdherentsService()
        {
            Name = _name;
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value;
                if (String.IsNullOrWhiteSpace(value))
                {
                    Debug.WriteLine("vide" + value);
                }
                if (!String.IsNullOrWhiteSpace(value))
                {
                    Debug.WriteLine("pas vide" + value);
                }
            }
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

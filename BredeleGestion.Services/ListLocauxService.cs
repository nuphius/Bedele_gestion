using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BredeleGestion.Services
{
    public class ListLocauxService
    {
        //Affichage de la liste des locaut ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private ObservableCollection<Locaux> _listLocaux = new ObservableCollection<Locaux>();

        public ObservableCollection<Locaux> ListLocaux
        {
            get { return _listLocaux; }
            set { _listLocaux = value; }
        }

        public ListLocauxService()
        {
            LoadListLocaux();
        }

        private void LoadListLocaux()
        {
            ConnexionBddService connexionBddService = new ConnexionBddService(RequetSqlService.SELECTBOX, RequetSqlService.TABLEBOX);
            List<DataRow> listBddBox = connexionBddService.ExecuteRequet();

            foreach (DataRow row in listBddBox)
            {
                _listLocaux.Add(new Locaux { Id = (int)row["boxid"], Name = row["boxname"].ToString(), Capacity = (int)row["boxcapacity"] });
            }

            _listLocaux = new ObservableCollection<Locaux>(_listLocaux.OrderBy(x => x.Name).ToList());
        }
    }

    public class Locaux
    {
        public int Id{ get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public override string ToString()
        {
            return $"{Name}       Capacité : {Capacity}";
        }
    }
}
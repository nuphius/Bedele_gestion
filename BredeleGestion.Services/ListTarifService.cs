using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace BredeleGestion.Services
{
    public class ListTarifService
    {
        //Affichage de la liste des tarifs ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private ObservableCollection<Price> _listPrice = new ObservableCollection<Price>();

        public ObservableCollection<Price> ListPrice
        {
            get { return _listPrice; }
            set { _listPrice = value; }
        }

        public ListTarifService()
        {
            LoadListPrice();
        }

        private void LoadListPrice()
        {
            ConnexionBddService connexionBddService = new ConnexionBddService(RequetSqlService.SELECTALLPRICE, RequetSqlService.TABLEPRICE);
            List<DataRow> listBddPrice = connexionBddService.ExecuteRequet();

            foreach (DataRow row in listBddPrice)
            {
                _listPrice.Add(new Price { Id = (int)row["priceid"], Name = row["pricename"].ToString(), Value = Convert.ToString(row["pricevalue"]) });
            }
        }
    }

    public class Price
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
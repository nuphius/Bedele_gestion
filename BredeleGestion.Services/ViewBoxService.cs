using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BredeleGestion.Services
{
    public class ViewBoxService
    {

        private ObservableCollection<Box> _listBox = new ObservableCollection<Box>();

        public ObservableCollection<Box> ListBox
        {
            get { return _listBox; }
            set { _listBox = value; }
        }

        //public List<string> ListEquip { get; set; } = new List<string>();


        public ViewBoxService()
        {
            LoadBoxBdd();
        }

        public void LoadBoxBdd()
        {
            ConnexionBddService connexionBddService = new ConnexionBddService(RequetSqlService.SELECTBOX, RequetSqlService.TABLEBOX);
            List<DataRow> listBoxBdd = connexionBddService.ExecuteRequet();

            foreach (DataRow box in listBoxBdd)
            {
                int id = (int)box[0];

                string optionEquip = LoadOption(id, string.Format(RequetSqlService.SELECTEQUIPBOXVIEW, id), RequetSqlService.TABLEEQUIPMENTBOX);
                string optionActiv = LoadOption(id, string.Format(RequetSqlService.SELECTACTIVBOXVIEW, id), RequetSqlService.TABLEACTIVITYBOX);

                ListBox.Add(new Box { Id = id, Name = box[1].ToString(), Capacity = (int)box[2], Surface = (int)box[5], OptionEquip = optionEquip, OptionActiv = optionActiv});
            }
        }

        private string LoadOption(int id, string requet, string table)
        {
            //Debug.WriteLine(requet);
            ConnexionBddService connexionBddService = new ConnexionBddService(requet, table);
            List<DataRow> listBoxBdd = connexionBddService.ExecuteRequet();

            if (listBoxBdd != null)
            {
                List<string> options = new List<string>();

                foreach (var item in listBoxBdd)
                {
                    options.Add(item[0].ToString());
                }

                return string.Join("\n", options);
            }
            else
            {
                return "Aucun";
            }
            
        }
    }

    public class Box
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Surface { get; set; }
        public string OptionEquip { get; set; }
        public string OptionActiv { get; set; }

    }
}

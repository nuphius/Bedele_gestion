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

        private Box _selectedBox;

        public Box SelectedBox
        {
            get { return _selectedBox; }
            set 
            { 
                _selectedBox = value;
            }
        }

        private ObservableCollection<Box> _listBox = new ObservableCollection<Box>();

        public ObservableCollection<Box> ListBox
        {
            get { return _listBox; }
            set { _listBox = value; }
        }

        public ViewBoxService(int idActivity = 0)
        {
            LoadBoxBdd(idActivity);
        }

        #region LoadBoxBdd
        /// <summary>
        /// Charge tous les informations des boxs et crée une liste d'objets Box observatable
        /// </summary>
        public void LoadBoxBdd(int idActivity = 0)
        {
            ListBox.Clear();
            string requet = string.Empty;

            if (idActivity != 0)
            {
                requet = string.Format(RequetSqlService.SELECTBOXFROMACTIVITY, idActivity, "");
            }
            else
            {
                requet = RequetSqlService.SELECTBOX;
            }

            ConnexionBddService connexionBddService = new ConnexionBddService(requet, RequetSqlService.TABLEBOX);
            List<DataRow> listBoxBdd = connexionBddService.ExecuteRequet();

            foreach (DataRow box in listBoxBdd)
            {
                string dateLockString = string.Empty;
                DateTime date;
                DateTime today = DateTime.Now.Date;

                if (!string.IsNullOrEmpty(box[4].ToString()))
                {
                    date = Convert.ToDateTime(box[4]);

                    Debug.WriteLine($"today :{today} date lock : {date}");

                    if (today >= date.Date)
                    {
                        dateLockString = "Disponible";

                        connexionBddService = new ConnexionBddService(string.Format(RequetSqlService.UPDATEDATEBOXNULL, box["boxid"].ToString()), RequetSqlService.TABLEBOX);
                        connexionBddService.InsertRequet();
                    }
                    else
                    {
                        dateLockString = $"Verrouillé jusqu'au : {date.ToString("dd/MM/yyyy")}";
                    }
                }
                else
                {
                    date = DateTime.MinValue;
                    dateLockString = "Disponible";  
                }

                int id = (int)box[0];

                string optionEquip = LoadOption(id, string.Format(RequetSqlService.SELECTEQUIPBOXVIEW, id), RequetSqlService.TABLEEQUIPMENTBOX);
                string optionActiv = LoadOption(id, string.Format(RequetSqlService.SELECTACTIVBOXVIEW, id), RequetSqlService.TABLEACTIVITYBOX);

                ListBox.Add(new Box { Id = id, Name = box[1].ToString(), Capacity = (int)box[2], DateLock = date, DateLockString = dateLockString, Surface = (int)box[5], OptionEquip = optionEquip, OptionActiv = optionActiv });
            }
        }
        #endregion

        #region LoadOption
        /// <summary>
        /// Récupère dans la BDD les options equipment et activity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="requet"></param>
        /// <param name="table"></param>
        /// <returns></returns>
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
        #endregion
    }

    public class Box
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public DateTime DateLock { get; set; }
        public string DateLockString { get; set; }
        public int Surface { get; set; }
        public string OptionEquip { get; set; }
        public string OptionActiv { get; set; }

    }
}

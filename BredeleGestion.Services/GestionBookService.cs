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
    public class GestionBookService
    {
        private List<Activitys> _listAdherent = new List<Activitys>();
        private ObservableCollection<BoxPlace> _listPlace = new ObservableCollection<BoxPlace>();

        public GestionBookService()
        {

        }

        #region LoadActivity
        /// <summary>
        /// Récupere toutes les activités de la BDD et retourne un list d'objet activity
        /// </summary>
        /// <returns></returns>
        public List<Activitys> LoadActivity()
        {
            ConnexionBddService connexionBddService = new ConnexionBddService(RequetSqlService.SELECTACTIVITY, RequetSqlService.TABLEACTIVITY);
            List<DataRow> listRstBdd = connexionBddService.ExecuteRequet();

            foreach (DataRow activity in listRstBdd)
            {
                try
                {
                    int id = int.Parse(activity["activid"].ToString());
                    decimal price = Convert.ToDecimal(activity["activincrease"]);

                    _listAdherent.Add(new Activitys { Id = id, Name = activity["activname"].ToString(), PriceAtivity = price });
                }
                catch (Exception ex)
                {
                    LogTools.AddLog(LogTools.LogType.ERREUR, "Problème de convertion des id de la bdd activity" + ex.Message);
                }
            }

            return _listAdherent;
        }
        #endregion

        #region LoadBoxs
        /// <summary>
        /// Récupération dans la BDD des Box en fonction de l'activité et bloqué ou non
        /// </summary>
        /// <param name="idActivity"></param>
        /// <param name="locked"></param>
        /// <returns></returns>
        public ObservableCollection<BoxPlace> LoadBoxs(int idActivity = 0, bool locked = false)
        {
            _listPlace.Clear();
            string requet = string.Empty;

            if (idActivity != 0)
            {
                requet = string.Format(RequetSqlService.SELECTBOXFROMACTIVITY, idActivity, "");
                if (locked)
                {
                    requet = string.Format(RequetSqlService.SELECTBOXFROMACTIVITY, idActivity, " AND boxendlock IS NULL");
                }
            }
            else
            {
                requet = RequetSqlService.SELECTBOX;
            }
            Debug.WriteLine(requet);
            ConnexionBddService connexionBddService = new ConnexionBddService(requet, RequetSqlService.TABLEBOX);
            List<DataRow> listRstBdd = connexionBddService.ExecuteRequet();

            foreach (DataRow dataRow in listRstBdd)
            {
                try
                {
                    int id = int.Parse(dataRow["boxid"].ToString());
                    int place = int.Parse(dataRow["boxcapacity"].ToString());

                    _listPlace.Add(new BoxPlace { Id = id, Name = dataRow["boxname"].ToString(), Place = place });
                }
                catch (Exception ex)
                {
                    LogTools.AddLog(LogTools.LogType.ERREUR, "Problème de convertion des id ou place de la bdd box" + ex.Message);
                }
            }
            return _listPlace;
        }
        #endregion

        #region SendBookBdd
        /// <summary>
        /// Envoi la réservation dans la BDD
        /// </summary>
        /// <param name="custId"></param>
        /// <param name="date"></param>
        /// <param name="hourstart"></param>
        /// <param name="hourend"></param>
        /// <param name="idBox"></param>
        /// <returns></returns>
        public bool SendBookBdd(int custId, string date, string hourstart, string hourend, int idBox)
        {
            string requet = string.Format(RequetSqlService.INSERTBOOK, date, hourstart, hourend, idBox, custId);

            ConnexionBddService connexionBddService = new ConnexionBddService(requet, RequetSqlService.TABLEBOOK);
            return connexionBddService.InsertRequet();
        }
        #endregion


    }

    public class Activitys
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceAtivity { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
    public class BoxPlace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Place { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}

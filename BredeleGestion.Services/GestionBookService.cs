using System;
using System.Collections.Generic;
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
        private List<BoxPlace> _listPlace = new List<BoxPlace>();

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

                    _listAdherent.Add(new Activitys { Id = id, Name = activity["activname"].ToString() });
                }
                catch (Exception ex)
                {
                    LogTools.AddLog(LogTools.LogType.ERREUR, "Problème de convertion des id de la bdd activity" + ex.Message);
                }
            }

            return _listAdherent;
        }
        #endregion

        public List<BoxPlace> LoadPlace()
        {
            ConnexionBddService connexionBddService = new ConnexionBddService(RequetSqlService.SELECTBOX, RequetSqlService.TABLEBOX);
            List<DataRow> listRstBdd = connexionBddService.ExecuteRequet();

            foreach (DataRow dataRow in listRstBdd)
            {
                try
                {
                    int id = int.Parse(dataRow["boxid"].ToString());
                    int place = int.Parse(dataRow["boxcapacity"].ToString());

                    _listPlace.Add(new BoxPlace { Id = id, Name = dataRow["boxname"].ToString(), Place = place});
                }
                catch (Exception ex)
                {
                    LogTools.AddLog(LogTools.LogType.ERREUR, "Problème de convertion des id ou place de la bdd box" + ex.Message);
                }
            }
            return _listPlace.OrderBy(x => x.Place).ToList();
        }

        public bool SendBookBdd(int custId, string date, string hourstart, string hourend, int idBox)
        {
            string requet = string.Format(RequetSqlService.INSERTBOOK, date, hourstart, hourend, idBox, custId);

            ConnexionBddService connexionBddService = new ConnexionBddService(requet, RequetSqlService.TABLEBOOK);
            return connexionBddService.InsertRequet();
        }
    }

    public class Activitys
    {
        public int Id { get; set; }
        public string Name { get; set; }

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

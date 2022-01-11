using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BredeleGestion.Services
{
    public class ConnexionBddService
    {
        #region Proriétés
        //Déclaration des propriétés
        private string _requeteSql;
        private string _table;
        private List<DataRow> _rstRequete;

        private string NameTable
        {
            get { return _table; }
            set { _table = value; }
        }

        private string RequeteSql
        {
            get { return _requeteSql; }
            set { _requeteSql = value; }
        }

        public ConnexionBddService(string requeteSql, string table)
        {
            RequeteSql = requeteSql;
            NameTable = table;
            _rstRequete = new List<DataRow>();
        }
        #endregion

        #region SQL Select --> Tableau des résultats
        /// <summary>
        /// Execute une requete SQL SELECT et retourne une liste tableau des résultats en DataRow
        /// </summary>
        /// <returns></returns>
        public List<DataRow> ExecuteRequet()
        {
            //Connection à la base de donnée
            SqlConnection connexion = new SqlConnection(@"Server=tcp:bredeleprojet.database.windows.net,1433;Initial Catalog=BredeleGestionBdd;Persist Security Info=False;User ID=bredele;Password=MIKAELmathieu21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            //constitution de la requete 
            SqlCommand selectCommand = new SqlCommand(_requeteSql, connexion);

            //Adaptation des données pour les lires
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataSet data = new DataSet();

            try
            {
                connexion.Open();
                //Récupère les données.
                adapter.Fill(data, _table);
            }
            catch (Exception ex)
            {
                LogTools.AddLog(LogTools.LogType.ERREUR, "Erreur de récupération dans la BDD" + ex.Message);
            }
            finally
            {
                connexion.Close();
            }

            foreach (DataRow row in data.Tables[_table].Rows)
            {
                _rstRequete.Add(row);
            }
            return _rstRequete;
        }
        #endregion
    }
}

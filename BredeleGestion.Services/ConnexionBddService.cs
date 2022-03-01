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

//#if DEBUG
//string a = @"Server=tcp:bredeleprojet.database.windows.net,1433;Initial Catalog=BredeleGestionBdd;Persist Security Info=False;User ID=bredele;Password=MIKAELmathieu21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
//#else
//string a = @"Server=tcp:bredeleprojet.database.windows.net,1433;Initial Catalog=BredeleGestionBdd;Persist Security Info=False;User ID=bredele;Password=MIKAELmathieu21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
//#endif

        private string _logConnexion = @"Server=tcp:bredeleprojet.database.windows.net,1433;Initial Catalog=BredeleGestionBdd;Persist Security Info=False;User ID=bredele;Password=MIKAELmathieu21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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

        public ConnexionBddService(string requeteSql = "", string table ="")
        {
            if (!string.IsNullOrEmpty(requeteSql))
            {
                RequeteSql = requeteSql;
                NameTable = table;
                _rstRequete = new List<DataRow>();
            }
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
            SqlConnection connexion = new SqlConnection(_logConnexion);

            //constitution de la requete 
            SqlCommand selectCommand = new SqlCommand(_requeteSql, connexion);

            //Adaptation des données pour les lires
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataSet data = new DataSet();

            int nbReturnLigne = 0;

            try
            {
                connexion.Open();
                //Récupère les données.
                nbReturnLigne = adapter.Fill(data, _table);
            }
            catch (Exception ex)
            {
                LogTools.AddLog(LogTools.LogType.ERREUR, "Erreur de récupération dans la BDD" + ex.Message);
            }
            finally
            {
                connexion.Close();
            }

            if (nbReturnLigne != 0)
            {
                foreach (DataRow row in data.Tables[_table].Rows)
                {
                    _rstRequete.Add(row);
                }

                return _rstRequete;
            }

            return null;
        }
        #endregion

        #region InsertRequet
        /// <summary>
        /// Execute une requete SQL INSERT, UPDATE ou DELETE et renvoi un BOOL
        /// </summary>
        /// <returns></returns>
        public bool InsertRequet()
        {
            SqlConnection connexion = new SqlConnection(_logConnexion);

            //constitution de la requete 
            SqlCommand selectCommand = new SqlCommand(_requeteSql, connexion);

            //Adaptation des données pour les lires
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
            DataSet data = new DataSet();

            int nbReturnLigne = 0;

            try
            {
                connexion.Open();
                //Récupère les données.
                nbReturnLigne = adapter.Fill(data, _table);
            }
            catch (Exception ex)
            {
                LogTools.AddLog(LogTools.LogType.ERREUR, "Erreur de récupération dans la BDD" + ex.Message);
            }
            finally
            {
                connexion.Close();
            }

            Debug.WriteLine(nbReturnLigne);

            if (nbReturnLigne != 0)
            {
                return false;
            }

            return true;
        }
        #endregion


        #region Check DataBase exist
        /// <summary>
        /// Check si la base de donnée existe
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public bool CheckDatabaseExists()
        {
            using (var connection = new SqlConnection(_logConnexion))
            {
                using (var command = new SqlCommand($"SELECT db_id('BredeleGestionBdd')", connection))
                {
                    connection.Open();
                    return (command.ExecuteScalar() != DBNull.Value);
                }
            }
        }
        #endregion

        #region CreateLocalDataBase
        /// <summary>
        /// Création en local d'une base de donnée
        /// </summary>
        /// <returns></returns>
        public bool CreateLocalDataBase()
        {
            string logConnection = "Server=localhost;Integrated security=SSPI;database=master";
            bool flag = true;
            String str;
            SqlConnection myConn = new SqlConnection(logConnection);

            str = LocalBdd.Bdd;

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {   myConn.Open();
                myCommand.ExecuteNonQuery();
                LogTools.AddLog(LogTools.LogType.TRACE, "Création BDD local réussite !");
                _logConnexion = logConnection;
            }
            catch (System.Exception ex)
            {
                LogTools.AddLog(LogTools.LogType.ERREUR, "Echec création d'un base de donnée local");
                flag = false;
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
            return flag;
        }
        #endregion
    }
}

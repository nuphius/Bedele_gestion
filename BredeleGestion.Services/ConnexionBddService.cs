using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BredeleGestion.Services
{
    public class ConnexionBddService
    {
        private string _requeteSql;
        private string _table;
        private List<string[]> _rstRequete;

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
            _rstRequete = new List<string[]>();
        }


        /// <summary>
        /// Execute une requete SQL SELECT et retourne une liste tableau des résultats
        /// </summary>
        /// <returns></returns>
        public List<string[]> ExecuteRequet()
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
                _rstRequete.Add(new string[] { row["usersname"].ToString(), row["userspassword"].ToString()});

                Debug.WriteLine(row["usersname"].ToString());
                Debug.WriteLine(row["userspassword"].ToString());
            }

            return _rstRequete;
        }
    }
}

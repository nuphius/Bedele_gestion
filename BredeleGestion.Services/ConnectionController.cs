using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BredeleGestion.Services
{
    public class ConnectionController

    {
        #region Authentification du logiciel
        /// <summary>
        /// Gestion de l'authentification du logiciel
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string ConnectUser(string login, string pwd)
        {
            try
            {
                login = login.Trim();
                pwd = pwd.Trim();

                //Si les champs sont vides
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pwd))
                {
                    LogTools.AddLog(LogTools.LogType.ERREUR, "Connexion - Login ou Mot de passe vide");
                    return "Erreur : Login ou mot de passe vide !";
                }
                else
                {
                    //Création de la requete et de la table souhaitée.
                    ConnexionBddService connexion = new ConnexionBddService("SELECT * FROM users", "users");

                    //Récupération d'une liste en DataRow .
                    List<DataRow> rstRequete = connexion.ExecuteRequet();

                    //Conversion de la liste en array liste et string afin de pouvour l'exploiter.
                    List<string[]> listRstRequete = rstRequete.Select(x => x.ItemArray.Select(y => y.ToString()).ToArray()).ToList();

                    //Si login ou password ne sont pas dans la BDD alors message d'erreur
                    if (!ControlUserServices.CheckLogin(login, listRstRequete) || !ControlUserServices.CheckPwd(pwd, ControlUserServices.pwdBdd))
                    {
                        LogTools.AddLog(LogTools.LogType.ERREUR, "Connexion - Login ou mot de passe incorrect !");
                        return "Login ou mot de passe incorrect !";
                    }

                    LogTools.AddLog(LogTools.LogType.TRACE, "Connexion - Identification réussite - " + ControlUserServices.userConnected);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la connection " + ex);
            }
            return "";
        }
        #endregion

        #region Démarrage de l'application
        /// <summary>
        /// Insertion dans le fichier log d'une ligne pour informer du démarrage du logiciel
        /// </summary>
        public void Load()
        {
            LogTools.AddLog(LogTools.LogType.TRACE, "---------- Démarrage de l'application ----------");
        }
        #endregion

        #region Fermeture de l'application
        /// <summary>
        /// Insertion dans le fichier log d'une ligne pour informer de la fermeture du logiciel
        /// </summary>
        public void Close()
        {
            LogTools.AddLog(LogTools.LogType.TRACE, "---------- Fermeture de l'application ----------");
        }
        #endregion
    }
}

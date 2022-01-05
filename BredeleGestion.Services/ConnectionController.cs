using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BredeleGestion.Services
{
    public class ConnectionController


    {/// <summary>
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

                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pwd))
                {
                    LogTools.AddLog(LogTools.LogType.ERREUR, "Connexion - Login ou Mot de passe vide");
                    return "Erreur : Login ou mot de passe vide !";
                }
                else
                {
                    ConnexionBddService connexion = new ConnexionBddService("SELECT * FROM users", "users");
                    List<string[]> rstRequete = connexion.ExecuteRequet();

                    if (!ControlUserServices.CheckLogin(login, rstRequete) || !ControlUserServices.CheckPwd(pwd, ControlUserServices.pwdBdd))
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

        public void Load()
        {
            LogTools.AddLog(LogTools.LogType.TRACE, "---------- Démarrage de l'application ----------");
        }

        public void Close()
        {
            LogTools.AddLog(LogTools.LogType.TRACE, "---------- Fermeture de l'application ----------");
        }
    }
}

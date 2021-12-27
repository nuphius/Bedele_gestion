using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bredele_Gestion.Controller
{
    internal class ConnectionController


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
                if (string.IsNullOrEmpty(login.Trim()) || string.IsNullOrEmpty(pwd.Trim()))
                {
                    LogTools.AddLog(LogTools.LogType.ERREUR, "Connexion - Login ou Mot de passe vide");
                    return "Erreur : Login ou mot de passe vide !";
                }

                if (true)
                {

                }
            }
            catch (Exception ex)
            {

                throw;
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

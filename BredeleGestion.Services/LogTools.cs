using System;
using System.IO;
using System.Windows;
using Bredele_Gestion;

namespace BredeleGestion.Services
{
    static class LogTools
    {
        const string LOG_TRACE = "TRACE";
        const string LOG_ERREUR = "ERREUR";

        public enum LogType
        {
            TRACE,
            ERREUR
        };

        static StreamWriter? _logFileStream;
        static string pathLog = Directory.GetCurrentDirectory() + "\\Log";

        public static void AddLog(LogType logType, string logMessage)
        {
            string fileName,lineMessage, displayType = string.Empty;

            switch (logType)
            {
                case LogType.TRACE:
                    displayType = LOG_TRACE;
                    break;
                case LogType.ERREUR:
                    displayType = LOG_ERREUR;
                    break;
                default:
                    break;
            }

            fileName = Path.Combine(pathLog, string.Format("Log-{0}.log", DateTime.Now.ToString("yyyy_MM_dd")));


            // vérification de l'existance du dossier LOG
            try
            {
                if (!Directory.Exists(fileName))
                {
                    Directory.CreateDirectory(pathLog);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problème de création du dossier log !" + ex);
                //MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                //Application.Current.Shutdown();
            }


            // Vérification de l'existance du fichier log suivant le nom :fileName
            try
            {
                if (!File.Exists(fileName))
                {
                    // Si le filchier n'a pas encore été créer aujourd'hui et que le stream n'est pas vide
                    //on le vide puis le recrée
                    if (_logFileStream != null)
                    {
                        _logFileStream.Close();
                        _logFileStream = null;
                    }
                }

                // Si le stream est vide on crée un file ou l'ouvre si il existe déjà
                if (_logFileStream == null)
                {
                    _logFileStream = File.AppendText(fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("problème création ou écriture dans le fichier log ! " + ex);
                //MessageBox.Show(ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                //Application.Current.Shutdown();
            }

            // Création de la ligne log a écrire dans le fichier suivant un format spécifique.
            lineMessage = String.Format("{0} - {1} : {2}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), displayType, logMessage);


            // Si stream et ligne à écrire sont non vide. Ecriture dans le fichier
            if (_logFileStream != null && !string.IsNullOrEmpty(lineMessage))
            {
                _logFileStream.WriteLine(lineMessage);
                _logFileStream.Flush();
            }
        }

    }
}

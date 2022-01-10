using Microsoft.VisualStudio.TestTools.UnitTesting;
using BredeleGestion.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Linq;

namespace TestUnitaireBredele
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAuthentificationUser()
        {
            ConnexionBddService connexion = new ConnexionBddService("SELECT * FROM users", "users");
            List<DataRow> rstRequete = connexion.ExecuteRequet();

            Debug.WriteLine(rstRequete);

            List<string[]> listRstRequete = rstRequete.Select(x => x.ItemArray.Select(y => y.ToString()).ToArray()).ToList();

            Debug.WriteLine(ControlUserServices.CheckLogin("truc", listRstRequete));
            Debug.WriteLine(ControlUserServices.CheckLogin("gerant", listRstRequete));
            Debug.WriteLine("récup pwd : " + ControlUserServices.pwdBdd);
            Debug.WriteLine("Pwd identique ? " + ControlUserServices.CheckPwd("cafe", ControlUserServices.pwdBdd));

            ConnectionController connectionController = new ConnectionController();
            string connectUser = connectionController.ConnectUser("gerant", "cafe");

            if (connectUser == "")
            {
                Debug.WriteLine("Connection Validée");
            }
            else
            {
                Debug.WriteLine("Erreur connextion");
            }
            
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using BredeleGestion.Services;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestUnitaireBredele
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ConnexionBddService connexion = new ConnexionBddService("SELECT * FROM users", "users");
            List<string[]> rstRequete = connexion.ExecuteRequet();

            
            Debug.WriteLine(ControlUserServices.CheckLogin("truc", rstRequete));
            Debug.WriteLine(ControlUserServices.CheckLogin("gerant", rstRequete));
            Debug.WriteLine("récup pwd : " + ControlUserServices.pwdBdd);
            Debug.WriteLine("Pwd identique ? "+ ControlUserServices.CheckPwd("cafe", ControlUserServices.pwdBdd));
        }
    }
}

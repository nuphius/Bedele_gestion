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
        #region Add log
        [TestMethod]
        public void loadbox()
        {
            GestionLocauxService gestionLocauxService = new GestionLocauxService();

            gestionLocauxService.LoadBox(8);

            Debug.WriteLine(gestionLocauxService.Name);
            Debug.WriteLine(gestionLocauxService.NbPlace);
            Debug.WriteLine(gestionLocauxService.Size);

        }
        #endregion

        #region NewPrice
        [TestMethod]
        public void NewPrice()
        {
            GestionTarifsService gestionTarifsService = new GestionTarifsService();

            gestionTarifsService.NewPrice = "1.2";
            Debug.WriteLine(gestionTarifsService.NewPrice + " 1");

            gestionTarifsService.NewPrice = "1";
            Debug.WriteLine(gestionTarifsService.NewPrice + " 2");

            gestionTarifsService.NewPrice = "1,2";
            Debug.WriteLine(gestionTarifsService.NewPrice + "  3");

            gestionTarifsService.NewPrice = "1235.2442";
            Debug.WriteLine(gestionTarifsService.NewPrice + " 4");

            gestionTarifsService.NewPrice = "1,2f";
            Debug.WriteLine(gestionTarifsService.NewPrice + " 6");

            gestionTarifsService.NewPrice = "1d,2";
            Debug.WriteLine(gestionTarifsService.NewPrice + " 7");

            gestionTarifsService.NewPrice = "1d";
            Debug.WriteLine(gestionTarifsService.NewPrice + " 8");

            gestionTarifsService.NewPrice = "d";
            Debug.WriteLine(gestionTarifsService.NewPrice + " 9");
        }
        #endregion

        #region List customer
        [TestMethod]
        public void ListCustomer()
        {
            ListAdherentService adherentService = new ListAdherentService();
            adherentService.FilterSearchAdherent = 3;

            foreach (var item in adherentService.ListCust)
            {
                Debug.WriteLine(item.ToString());
            }

        }
        #endregion

        #region authentification user
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
        #endregion

        #region Add user
        [TestMethod]
        public void TestAddUsers()
        {
            GestionAdherentsService user = new GestionAdherentsService();
            user.Name = "ekjln";
            Debug.WriteLine(user.Name);

            user.SelectCity("68350");
            Debug.WriteLine(user.City);

            user.BirthDate = "09/10/1984";
            Debug.WriteLine(user.BirthDate);

            //Debug.WriteLine(user.AddUser(true, "Mr"));
        }
        #endregion
    }
}

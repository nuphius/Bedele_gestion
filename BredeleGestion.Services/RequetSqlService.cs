using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BredeleGestion.Services
{
    public static class RequetSqlService
    {

        #region requet activitybox

        public const string INSERTACTIVITYBOX = "INSERT INTO activitybox(activboxpricename, activboxpricevalue, fkboxid, fkactiid) VALUES ('0', '0','{0}','{1}');";
        public const string TABLEACTIVITYBOX = "activitybox";
        public const string DELETEACTIVITYBOXID = "DELETE FROM activitybox WHERE fkboxid={0}";

        #endregion

        #region requet equipmentBox

        public const string INSERTEQUIPMENTBOX = "INSERT INTO equipmentbox(equipboxqte, fkboxid, fkequipid) VALUES ('0','{0}','{1}');";
        public const string TABLEEQUIPMENTBOX = "equipmentbox";
        public const string DELETEEQUIPMENTBOXID = "DELETE FROM equipmentbox WHERE fkboxid={0}";

        #endregion

        #region requet box
        /// <summary>
        /// supprimer la box suivant l'id en argument
        /// </summary>
        public const string DELETEBOX = "DELETE FROM box WHERE boxid={0}";
        /// <summary>
        /// Mise a jour des champs d'une box avec 0->boxname 1->boxcapacity 2->boxsurface 3->where boxid
        /// </summary>
        public const string UPDATEBOX = "UPDATE box SET boxname='{0}', boxcapacity='{1}', boxsurface='{2}' " +
            "WHERE boxid={3}";

        /// <summary>
        /// Selectionne la box avec tous les jointures
        /// </summary>
        public const string SELECTBOXINNEREQUIPMENT = "SELECT boxname, boxcapacity, boxsurface, fkequipid FROM box " +
            "INNER JOIN equipmentbox ON boxid=equipmentbox.fkboxid WHERE boxid={0}";
        public const string SELECTBOXINNERACTIVITY = "SELECT fkactiid  FROM box " +
            "INNER JOIN activitybox ON boxid=activitybox.fkboxid WHERE boxid={0}";

        /// <summary>
        /// Selectionne tous les champs de la table box avec l'id en parametre WHERE
        /// </summary>
        public const string SELECTBOXWITHID = "SELECT * FROM box WHERE boxid={0}";

        /// <summary>
        /// Selectection de tous les champs box
        /// </summary>
        public const string SELECTBOX = "SELECT * FROM box";
        /// <summary>
        /// INSERT INTO box(boxname, boxcapacity, boxsurface)
        /// </summary>
        public const string INSERBOX = "INSERT INTO box(boxname, boxcapacity, boxsurface) " +
            "VALUES ('{0}','{1}','{2}');";
        /// <summary>
        /// selectionne l'ID d'une box en fonction de son name
        /// </summary>
        public const string SELECTBOXID = "SELECT boxid FROM box WHERE boxname='{0}';";
        public const string TABLEBOX = "box";
        #endregion

        #region requete equipment
        /// <summary>
        /// Selectionne tous les champs de la table equipment
        /// </summary>
        public const string SELECTEQUIPMENTS = "SELECT * FROM equipment";
        public const string TABLEEQUIPMENT = "equipment";


        //public const string INSER
        #endregion

        #region requet activity
        /// <summary>
        /// Sélctionne tous les champs de la table activité
        /// </summary>
        public const string SELECTACTIVITY = "SELECT * FROM activity";
        public const string TABLEACTIVITY = "activity";
        #endregion

        #region Ajout/modifier prix

        public const string DELETEPRICE = "DELETE FROM price WHERE priceid={0}";
        /// <summary>
        /// Selectionne tous les éléments de la table price
        /// </summary>
        public const string SELECTALLPRICE = "SELECT * FROM price";
        /// <summary>
        /// Ajouter un prix dans la BDD
        /// </summary>
        public const string ADDPRICE = "INSERT INTO price(pricename, pricevalue) VALUES ('{0}', {1})";
        /// <summary>
        /// Mettre ) jour un prix de la BDD suivant l'ID en parametre
        /// </summary>
        public const string UPDATEPRICE = "UPDATE price SET pricename='{0}', pricevalue={1} WHERE priceid={2}";
        /// <summary>
        /// table price
        /// </summary>
        public const string TABLEPRICE = "price";

        #endregion

        #region filtre adherent
        public const string COUNTADHERENT = "SELECT count(*) AS nb FROM customer WHERE custadherent='true'";
        public const string COUNTNOADHERENT = "SELECT count(*) AS nb FROM customer WHERE custadherent='false'";
        public const string SELECTCUSTSEARCH = "SELECT custid, custname, custfirstname FROM customer";
        #endregion

        #region Table User
        /// <summary>
        /// Sélectionne les utilisateurs
        /// </summary>
        public const string SELECTALLUSERS = "SELECT * FROM users";

        /// <summary>
        /// Sélectionne toutes les villes, CP et Id de la table
        /// </summary>
        public const string SELECTCPCITY = "SELECT * FROM city WHERE addpostal=";
        public const string TABLECITY = "city";

        /// <summary>
        /// Selectionne le customer suivant l'id donné
        /// </summary>
        public const string SELECTUSER = "SELECT * FROM customer INNER JOIN city ON fkcityid=addid WHERE custid=";

        /// <summary>
        /// Update des informations d'un utilisateur en particulier (10 parametres).
        /// </summary>
        public const string UPDATECUST = "UPDATE customer SET custcivility={0}, custname={1}, custfirstname={2}," +
        " custphone={3}, custmail={4}, custbirthdate={5}, custadherent={6}, fkcityid={7}, custaddress={8}, custaddress2={9} WHERE custid={10}";

        /// <summary>
        /// Suppression d'un utilisateur en fonction de l'id (2 parametres)
        /// </summary>
        public const string DELETECUST = "DELETE FROM {0} WHERE custid={1}";

        /// <summary>
        /// Ajouter un customer dans la table
        /// </summary>
        public const string TABLECUST = "customer";
        public const string ADDCUST = "INSERT INTO customer(" +
        "custcivility, custname, custfirstname, custphone, custmail, custbirthdate, custadherent, fkcityid, custaddress, custaddress2) VALUES ";

        /// <summary>
        /// Supprime un utilisateur
        /// </summary>

        
        #endregion
    }
}

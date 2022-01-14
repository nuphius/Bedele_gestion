using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BredeleGestion.Services
{
    public static class RequetSqlService
    {
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
        /// Update des des information d'un utilisateur en particulier.
        /// </summary>
        public const string UPDATECUST = "UPDATE customer SET custcivility={0}, custname={1}, custfirstname={2}," +
        " custphone={3}, custmail={4}, custbirthdate={5}, custadherent={6}, fkcityid={7}, custaddress={8}, custaddress2={9} WHERE custid={10}";

        /// <summary>
        /// Suppression d'un utilisateur en fonction de l'id
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

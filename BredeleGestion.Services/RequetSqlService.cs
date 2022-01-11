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
        /// Supprime un utilisateur
        /// </summary>
        public const string ADDUSER = ".....";

        public const string DELETEUSER = ".....";
        #endregion
    }
}

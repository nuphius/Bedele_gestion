using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BredeleGestion.Services
{
    static class RequestSqlService
    {
        #region Table User
        /// <summary>
        /// Sélectionne les utilisateurs
        /// </summary>
        public const string SELECTALLUSERS = "SELECT * FROM users";

        /// <summary>
        /// Supprime un utilisateur
        /// </summary>
        public const string ADDUSER = ".....";

        public const string DELETEUSER = ".....";
        #endregion
    }
}

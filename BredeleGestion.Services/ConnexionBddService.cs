using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BredeleGestion.Services
{
    public class ConnexionBddService
    {
        SqlConnection connexion = new SqlConnection(@"Server=tcp:bredeleprojet.database.windows.net,1433;Initial Catalog=BredeleGestionBdd;Persist Security Info=False;User ID=bredele;Password=MIKAELmathieu21;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    }
}

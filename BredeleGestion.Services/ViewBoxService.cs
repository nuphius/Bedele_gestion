using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BredeleGestion.Services
{
    public class ViewBoxService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Surface { get; set; }

        public ViewBoxService()
        {

        }

        public List<ViewBoxService> LoadBoxBdd()
        {
            ConnexionBddService connexionBddService = new ConnexionBddService(RequetSqlService.SELECTBOX, RequetSqlService.TABLEBOX);
            List<DataRow> listBoxBdd = connexionBddService.ExecuteRequet();

            List<ViewBoxService> listBox = new List<ViewBoxService>();

            foreach (DataRow box in listBoxBdd)
            {
                listBox.Add(new ViewBoxService { Id = (int)box[0], Name = box[1].ToString(), Capacity = (int)box[2], Surface = (int)box[5] });
            }

            return listBox;
        }
    }

    public class Box
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Surface { get; set; }

    }
}

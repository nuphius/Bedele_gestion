using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BredeleGestion.Services
{
    public class ViewBookService
    {
        private List<BookBox> _listeBookBox = new List<BookBox>();

        public ViewBookService(int id = 0)
        {
            //return LoadBookBox(id);
        }

        public List<BookBox> LoadBookBox(int id)
        {
            string requet = string.Format(RequetSqlService.SELECTBOOK, id);
            ConnexionBddService connexionBddService = new ConnexionBddService(requet, RequetSqlService.TABLEBOX);
            List<DataRow> listRstBdd = connexionBddService.ExecuteRequet();

            if (listRstBdd != null)
            {
                foreach (DataRow row in listRstBdd)
                {
                    try
                    {
                        string dayBook = DateTime.Parse(row["bookbegindate"].ToString()).ToString("dd/MM/yyyy");
                        string hourStartBook = DateTime.Parse(row["bookbegintime"].ToString()).Hour.ToString();
                        string hoursEndBook = DateTime.Parse(row["bookendtime"].ToString()).Hour.ToString();
                        int idBook = int.Parse(row["bookid"].ToString());
                        int idCust = int.Parse(row["custid"].ToString());
                        string nameCust = row["custname"].ToString();
                        string firstNameCust = row["custfirstname"].ToString();

                        _listeBookBox.Add(new BookBox
                        {
                            IdBox = id,
                            NameBox = row["boxname"].ToString(),
                            DayBook = dayBook,
                            HoursEndBook = hoursEndBook,
                            HourStartBook = hourStartBook,
                            IdBook = idBook,
                            IdCust = idCust,
                            NameCust = nameCust,
                            FirstNameCust = firstNameCust
                        });

                    }
                    catch (Exception ex)
                    {
                        LogTools.AddLog(LogTools.LogType.ERREUR, "Problème de convertion lors de la récupération des réservations" + ex.Message);
                    }
                }
            }
            return _listeBookBox;
        }
    }

    public class BookBox
    {
        public int IdBook { get; set; }
        public string DayBook { get; set; }
        public string HourStartBook { get; set; }
        public string HoursEndBook { get; set; }
        public int IdBox { get; set; }
        public string NameBox { get; set; }
        public string FirstNameCust { get; set; }
        public int IdCust { get; set; }
        public string NameCust { get; set; }
    }
}

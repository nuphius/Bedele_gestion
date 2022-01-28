using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BredeleGestion.Services;

namespace Bredele_Gestion.viewmodel
{
    public class ViewListBookViewModel
    {
        private int _idbox;
        //private List<BookBox> _listBookBox { get; set; }

        public List<BookBox> ListBookBox { get; set; }

        public ViewListBookViewModel(int idBox = 0)
        {
            _idbox = idBox;
            ViewBookService bookService = new ViewBookService(_idbox);

            ListBookBox = bookService.LoadBookBox(_idbox);
        }
    }
}

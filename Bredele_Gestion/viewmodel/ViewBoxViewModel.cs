using BredeleGestion.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bredele_Gestion.viewmodel
{
    public class ViewBoxViewModel
    {
        public ObservableCollection<Box> ListBox { get; set; }

        //public Box SelectedBox
        //{
        //    get { return _selectedBox; }
        //    set
        //    {
        //        _selectedBox = value;
        //    }
        //}
        public ViewBoxViewModel()
        {
            ViewBoxService viewBox = new ViewBoxService();
            ListBox = viewBox.ListBox;

        }
    }
}

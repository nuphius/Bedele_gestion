using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;

namespace BredeleGestion.Services
{
    public class GestionLocauxService : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region NotifyChanged
        /// <summary>
        /// Fonction de gestion des propriétés modifiés
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}

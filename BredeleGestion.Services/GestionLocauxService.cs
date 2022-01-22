﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace BredeleGestion.Services
{
    public class GestionLocauxService : INotifyPropertyChanged
    {
        private List<ActivEquip> _listEquipments = new List<ActivEquip>();
        private List<ActivEquip> _listActivitys = new List<ActivEquip>();

        private List<int> _listIdEquipments = new List<int>();
        private List<int> _listIdActivitys = new List<int>();

        private string _name;
        private string _nbPlace;
        private string _size;

        #region private de nameEquipX et nameactivX
        private string _nameEquip1;
        private string _nameEquip2;
        private string _nameEquip3;
        private string _nameEquip4;
        private string _nameEquip5;
        private string _nameEquip6;

        private string _nameActiv1;
        private string _nameActiv2;
        private string _nameActiv3;
        private string _nameActiv4;
        private string _nameActiv5;
        private string _nameActiv6;
        #endregion

        #region private checkEquipX et checkActivX
        private bool _checkEquip1;
        private bool _checkEquip2;
        private bool _checkEquip3;
        private bool _checkEquip4;
        private bool _checkEquip5;
        private bool _checkEquip6;

        private bool _checkActiv1;
        private bool _checkActiv2;
        private bool _checkActiv3;
        private bool _checkActiv4;
        private bool _checkActiv5;
        private bool _checkActiv6;
        #endregion

        #region prorpiete Name, Nbplace, Size
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string NbPlace
        {
            get { return _nbPlace; }
            set
            {
                if (CheckOnlyNumber(value))
                {
                    _nbPlace = value;
                }
                else
                {
                    if (!string.IsNullOrEmpty(_nbPlace) && string.IsNullOrEmpty(value))
                    {
                        _nbPlace = string.Empty;
                        value = string.Empty;
                    }
                    else
                    {
                        value = _nbPlace;
                    }
                }
                _nbPlace = value;
                this.NotifyPropertyChanged(nameof(NbPlace));
            }
        }

        public string Size
        {
            get { return _size; }
            set
            {
                if (CheckOnlyNumber(value))
                {
                    _size = value;
                }
                else
                {
                    if (!string.IsNullOrEmpty(_size) && string.IsNullOrEmpty(value))
                    {
                        _size = string.Empty;
                        value = string.Empty;
                    }
                    else
                    {
                        value = _size;
                    }
                }

                _size = value;
                this.NotifyPropertyChanged(nameof(Size));
            }
        }
        #endregion

        #region Propriété NameEquip
        public string NameEquip1
        {
            get { return _nameEquip1; }
            set
            {
                _nameEquip1 = value;
                this.NotifyPropertyChanged(nameof(NameEquip1));
            }
        }

        public string NameEquip2
        {
            get { return _nameEquip2; }
            set
            {
                _nameEquip2 = value;
                this.NotifyPropertyChanged(nameof(NameEquip2));
            }
        }

        public string NameEquip3
        {
            get { return _nameEquip3; }
            set
            {
                _nameEquip3 = value;
                this.NotifyPropertyChanged(nameof(NameEquip3));
            }
        }

        public string NameEquip4
        {
            get { return _nameEquip4; }
            set
            {
                _nameEquip4 = value;
                this.NotifyPropertyChanged(nameof(NameEquip4));
            }
        }

        public string NameEquip5
        {
            get { return _nameEquip5; }
            set
            {
                _nameEquip5 = value;
                this.NotifyPropertyChanged(nameof(NameEquip5));
            }
        }

        public string NameEquip6
        {
            get { return _nameEquip6; }
            set
            {
                _nameEquip6 = value;
                this.NotifyPropertyChanged(nameof(NameEquip6));
            }
        }
        #endregion

        #region Propriété NameActiv

        public string NameActiv1
        {
            get { return _nameActiv1; }
            set { _nameActiv1 = value; }
        }

        public string NameActiv2
        {
            get { return _nameActiv2; }
            set { _nameActiv2 = value; }
        }

        public string NameActiv3
        {
            get { return _nameActiv3; }
            set { _nameActiv3 = value; }
        }

        public string NameActiv4
        {
            get { return _nameActiv4; }
            set { _nameActiv4 = value; }
        }

        public string NameActiv5
        {
            get { return _nameActiv5; }
            set { _nameActiv5 = value; }
        }

        public string NameActiv6
        {
            get { return _nameActiv6; }
            set { _nameActiv6 = value; }
        }


        #endregion

        #region Propriété CheckEquip
        public bool CheckEquip1
        {
            get { return _checkEquip1; }
            set
            {
                _checkEquip1 = value;
                CheckEquipment(NameEquip1, value);
                this.NotifyPropertyChanged(nameof(CheckEquip1));
            }
        }

        public bool CheckEquip2
        {
            get { return _checkEquip2; }
            set
            {
                _checkEquip2 = value;
                CheckEquipment(NameEquip2, value);
                this.NotifyPropertyChanged(nameof(CheckEquip2));
            }
        }
        public bool CheckEquip3
        {
            get { return _checkEquip3; }
            set
            {
                _checkEquip3 = value;
                CheckEquipment(NameEquip3, value);
                this.NotifyPropertyChanged(nameof(CheckEquip3));
            }
        }

        public bool CheckEquip4
        {
            get { return _checkEquip4; }
            set
            {
                _checkEquip4 = value;
                CheckEquipment(NameEquip4, value);
                this.NotifyPropertyChanged(nameof(CheckEquip4));
            }
        }

        public bool CheckEquip5
        {
            get { return _checkEquip5; }
            set
            {
                _checkEquip5 = value;
                CheckEquipment(NameEquip5, value);
                this.NotifyPropertyChanged(nameof(CheckEquip5));
            }
        }
        public bool CheckEquip6
        {
            get { return _checkEquip6; }
            set
            {
                _checkEquip6 = value;
                CheckEquipment(NameEquip6, value);
                this.NotifyPropertyChanged(nameof(CheckEquip6));
            }
        }
        #endregion

        #region Propriété CheckActiv
        public bool CheckActiv1
        {
            get { return _checkActiv1; }
            set
            {
                _checkActiv1 = value;
                CheckActivity(NameActiv1, value);
                this.NotifyPropertyChanged(nameof(CheckActiv1));
            }
        }

        public bool CheckActiv2
        {
            get { return _checkActiv2; }
            set
            {
                _checkActiv2 = value;
                CheckActivity(NameActiv2, value);
                this.NotifyPropertyChanged(nameof(CheckActiv2));
            }
        }
        public bool CheckActiv3
        {
            get { return _checkActiv3; }
            set
            {
                _checkActiv3 = value;
                CheckActivity(NameActiv3, value);
                this.NotifyPropertyChanged(nameof(CheckActiv3));
            }
        }

        public bool CheckActiv4
        {
            get { return _checkActiv4; }
            set
            {
                _checkActiv4 = value;
                CheckActivity(NameActiv4, value);
                this.NotifyPropertyChanged(nameof(CheckActiv4));
            }
        }
        public bool CheckActiv5
        {
            get { return _checkActiv5; }
            set
            {
                _checkActiv5 = value;
                CheckActivity(NameActiv5, value);
                this.NotifyPropertyChanged(nameof(CheckActiv5));
            }
        }
        public bool CheckActiv6
        {
            get { return _checkActiv6; }
            set
            {
                _checkActiv6 = value;
                CheckActivity(NameActiv6, value);
                this.NotifyPropertyChanged(nameof(CheckActiv6));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public GestionLocauxService()
        {
            _listActivitys = LoadEquipActiv(RequetSqlService.SELECTACTIVITY, RequetSqlService.TABLEACTIVITY);
            _listEquipments = LoadEquipActiv(RequetSqlService.SELECTEQUIPMENTS, RequetSqlService.TABLEEQUIPMENT);

            _nameEquip1 = _listEquipments[0].Name;
            _nameEquip2 = _listEquipments[1].Name;
            _nameEquip3 = _listEquipments[2].Name;
            _nameEquip4 = _listEquipments[3].Name;
            _nameEquip5 = _listEquipments[4].Name;
            _nameEquip6 = _listEquipments[5].Name;

            _nameActiv1 = _listActivitys[0].Name;
            _nameActiv2 = _listActivitys[1].Name;
            _nameActiv3 = _listActivitys[2].Name;
            _nameActiv4 = _listActivitys[3].Name;
        }

        public bool InsertUpdateBox(int id = 0)
        {
            string requetBox;
            int boxId;

            try
            {
                if (id == 0)
                {
                    requetBox = string.Format(RequetSqlService.INSERBOX, _name, _nbPlace, _size);
                }
                else
                {
                    requetBox = string.Format(RequetSqlService.INSERBOX, _name, _nbPlace, _size);
                }

                ConnexionBddService connexionBddService = new ConnexionBddService(requetBox, RequetSqlService.TABLEBOX);

                if (connexionBddService.InsertRequet())
                {
                    if (id == 0)
                    {
                        string requetEquipBox= string.Empty;
                        string requetActivBox = string.Empty;

                        connexionBddService = new ConnexionBddService(string.Format(RequetSqlService.SELECTBOXID, _name), RequetSqlService.TABLEBOX);
                        List<DataRow> listRstRequet = new List<DataRow>(connexionBddService.ExecuteRequet());

                        boxId = (int)listRstRequet[0]["boxid"];

                        foreach (var equip in _listIdEquipments)
                        {
                            requetEquipBox += string.Format(RequetSqlService.INSERTEQUIPMENTBOX, boxId, equip);
                        }

                        foreach (var activ in _listIdActivitys)
                        {
                            requetActivBox += string.Format(RequetSqlService.INSERTACTIVITYBOX, boxId, activ);
                        }

                        connexionBddService = new ConnexionBddService(requetEquipBox, RequetSqlService.TABLEEQUIPMENTBOX);

                        if (connexionBddService.InsertRequet())
                        {
                            connexionBddService = new ConnexionBddService(requetActivBox, RequetSqlService.TABLEEQUIPMENTBOX);

                            if (connexionBddService.InsertRequet())
                            {
                                return true;
                            }
                            else
                            {
                                LogTools.AddLog(LogTools.LogType.ERREUR, "Echec de l'insertion de activityBox dans la BDD");
                            }
                        }
                        else
                        {
                            LogTools.AddLog(LogTools.LogType.ERREUR, "Echec de l'insertion de equipmentBox dans la BDD");
                        }
                    }
                    else
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    LogTools.AddLog(LogTools.LogType.ERREUR, "Echec de l'insertion de box dans la BDD");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                LogTools.AddLog(LogTools.LogType.ERREUR, "ERREUR - lors de l'execution de la fonction InsertBox." + ex.Message);
                return false;
            }
        }

        #region CheckAvtivity
        /// <summary>
        /// Ajoute ou supprime dans une liste les ID des ativitys sélectionnées
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        public void CheckActivity(string name, bool state)
        {
            var filter = _listActivitys.Where(x => x.Name == name).Select(y => y.Id);

            if (state)
            {
                foreach (var item in filter)
                {
                    _listIdActivitys.Add(item);
                }
            }
            else
            {
                foreach (var item in filter)
                {
                    _listIdActivitys.Remove(item);
                }
            }
        }
        #endregion

        #region CheckEquipment
        /// <summary>
        /// Ajoute ou supprime dans une liste les ID des équipements sélectionnées
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        public void CheckEquipment(string name, bool state)
        {
            var filter = _listEquipments.Where(x => x.Name == name).Select(y => y.Id);

            if (state)
            {
                foreach (var item in filter)
                {
                    _listIdEquipments.Add(item);
                }
            }
            else
            {
                foreach (var item in filter)
                {
                    _listIdEquipments.Remove(item);
                }
            }
        }
        #endregion

        #region Fonction de récupération des tables equipment et activity
        /// <summary>
        /// Fonction de récupération des tables equipment et activity
        /// </summary>
        /// <param name="requetSql"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private List<ActivEquip> LoadEquipActiv(string requetSql, string table)
        {
            List<ActivEquip> listRstSql = new List<ActivEquip>();

            ConnexionBddService connexionBddService = new ConnexionBddService(requetSql, table);
            List<DataRow> listRstRequet = new List<DataRow>(connexionBddService.ExecuteRequet());

            foreach (DataRow row in listRstRequet)
            {
                listRstSql.Add(new ActivEquip { Id = (int)row[0], Name = row[1].ToString() });
            }

            return listRstSql;
        }
        #endregion

        #region CheckOnlyNumber Fonction pour vérifier si seulement des chiffres sont saisies
        /// <summary>
        /// Fonction pour vérifier si seulement des chiffres sont saisies pour certains champs
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool CheckOnlyNumber(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Regex regex = new Regex(@"^[0-9]*[0-9]$");

                if (regex.IsMatch(value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;

        }
        #endregion

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

    public class ActivEquip
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}

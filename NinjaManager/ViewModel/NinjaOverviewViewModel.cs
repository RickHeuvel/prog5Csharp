using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaManager.ViewModel
{
    public class NinjaOverviewViewModel: ViewModelBase
    {
        private MainViewModel _mainModel;

        private NinjaViewModel _selectedNinja;

        public NinjaViewModel SelectedNinja
        {
            get { return _selectedNinja; }
            set { _selectedNinja = value;  }
        }

        private EquipmentViewModel _headEquipment;

        public EquipmentViewModel HeadEquipment
        {
            get { return _headEquipment; }
            set { _headEquipment = value; }
        }
        private EquipmentViewModel _shoulderEquipment;

        public EquipmentViewModel ShoulderEquipment
        {
            get { return _shoulderEquipment; }
            set { _shoulderEquipment = value;}
        }

        private EquipmentViewModel _chestEquipment;

        public EquipmentViewModel ChestEquipment
        {
            get { return _chestEquipment; }
            set { _chestEquipment = value; }
        }

        private EquipmentViewModel _beltEquipment;

        public EquipmentViewModel BeltEquipment
        {
            get { return _beltEquipment; }
            set { _beltEquipment = value;}
        }

        private EquipmentViewModel _legsEquipment;

        public EquipmentViewModel LegsEquipment
        {
            get { return _legsEquipment; }
            set { _legsEquipment = value;}
        }

        private EquipmentViewModel _bootsEquipment;

        public EquipmentViewModel BootsEquipment
        {
            get { return _bootsEquipment; }
            set { _bootsEquipment = value;}
        }

        private ObservableCollection<EquipmentViewModel> _equipment;

        public ObservableCollection<EquipmentViewModel> Equipment
        {
            get { return _equipment; }
            set { _equipment = value;}
        }




        public NinjaOverviewViewModel(MainViewModel main)
        {

            _mainModel = main;
            Equipment = new ObservableCollection<EquipmentViewModel>();
            SelectedNinja = _mainModel.SelectedNinja;

            getNinjaEquipment();
          
        }

        private void getNinjaEquipment()
        {
            List<int> ids = new List<int>();

            // get all ids of the equipment belonging to ninja
            SelectedNinja.Equipments.ToList().ForEach(e => ids.Add(e.Id));

            // get all equipment from main equipmentlist matching id
            ids.ForEach(i => Equipment.Add(_mainModel.Equipment.Where(e => e.Id == i).First()));

            SetEquipmentTypes();
        }

        //check categorie and fill correct object 
        private void SetEquipmentTypes()
        {
            foreach (var item in Equipment)
            {
                switch (item.Category.Name)
                {
                    case "Head":
                        HeadEquipment = item;
                        break;
                    case "Shoulders":
                        ShoulderEquipment = item;
                        break;
                    case "Chest":
                        ChestEquipment = item;
                        break;
                    case "Belt":
                        BeltEquipment = item;
                        break;
                    case "Legs":
                        LegsEquipment = item;
                        break;
                    case "Boots":
                        BootsEquipment = item;
                        break;

                    default:
                        break;
                }
            }
        }


    }
}

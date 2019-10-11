using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class NinjaOverviewViewModel: ViewModelBase
    {
        private MainViewModel _mainModel;

        public NinjaViewModel SelectedNinja { get; set; }

        public EquipmentViewModel HeadEquipment { get; set; }

        public EquipmentViewModel ShoulderEquipment { get; set; }

        public EquipmentViewModel ChestEquipment { get; set; }

        public EquipmentViewModel BeltEquipment { get; set; }

        public EquipmentViewModel LegsEquipment { get; set; }

        public EquipmentViewModel BootsEquipment { get; set; }

        public ObservableCollection<EquipmentViewModel> Equipment { get; set; }


        public ICommand ShowShopCommand { get; set; }

        private ShopWindow _shopWindow;


        public NinjaOverviewViewModel(MainViewModel main)
        {

            _mainModel = main;
            Equipment = new ObservableCollection<EquipmentViewModel>();
            SelectedNinja = _mainModel.SelectedNinja;

            GetNinjaEquipment();

            ShowShopCommand = new RelayCommand(ShowShop);
         
        }


        private void GetNinjaEquipment()
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

        private void ShowShop()
        {
            _shopWindow = new ShopWindow();
            _shopWindow.Show();
        }
    }
}

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

        //public NinjaViewModel SelectedNinja { get; set; }

        private NinjaViewModel _selectedNinja;

        public NinjaViewModel SelectedNinja
        {
            get { return _selectedNinja; }
            set { _selectedNinja = value; SetEquipmentTypes(); }
        }


        public EquipmentViewModel HeadEquipment { get; set; }

        public EquipmentViewModel ShoulderEquipment { get; set; }

        public EquipmentViewModel ChestEquipment { get; set; }

        public EquipmentViewModel BeltEquipment { get; set; }

        public EquipmentViewModel LegsEquipment { get; set; }

        public EquipmentViewModel BootsEquipment { get; set; }

        public ObservableCollection<EquipmentViewModel> Equipment 
        {
            get { return SelectedNinja.Equipments; }
            set { SelectedNinja.Equipments = value; SetEquipmentTypes(); } 
        }


        public ICommand ShowShopCommand { get; set; }

        private ShopWindow _shopWindow;


        public NinjaOverviewViewModel(MainViewModel main)
        {

            _mainModel = main;

            SelectedNinja = _mainModel.SelectedNinja;
            Equipment = new ObservableCollection<EquipmentViewModel>();

            ShowShopCommand = new RelayCommand(ShowShop);
         
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

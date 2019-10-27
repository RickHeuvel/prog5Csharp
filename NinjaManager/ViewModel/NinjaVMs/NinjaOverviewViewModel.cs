using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.View;
using NinjaManager.ViewModel.NinjaVMs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class NinjaOverviewViewModel: ViewModelBase
    {
        private ManageNinjasViewModel _manageNinjasModel;

        private NinjaViewModel _selectedNinja { get; set; }
        public NinjaViewModel SelectedNinja
        {
            get { return _selectedNinja; }
            set { _selectedNinja = value; SetEquipmentTypes();}
        }


        private EquipmentViewModel _headEquipment;

        public EquipmentViewModel HeadEquipment
        {
            get { return _headEquipment; }
            set { _headEquipment = value; RaisePropertyChanged(); }
        }
        public EquipmentViewModel _shoulderEquipment;
        public EquipmentViewModel ShoulderEquipment
        {
            get { return _shoulderEquipment; }
            set { _shoulderEquipment = value; RaisePropertyChanged(); }
        }

        public EquipmentViewModel _chestEquipment;
        public EquipmentViewModel ChestEquipment
        {
            get { return _chestEquipment; }
            set { _chestEquipment = value; RaisePropertyChanged(); }
        }

        public EquipmentViewModel _beltEquipment;
        public EquipmentViewModel BeltEquipment 
        {
            get { return _beltEquipment; }
            set { _beltEquipment = value; RaisePropertyChanged(); }
        }

        public EquipmentViewModel _legsEquipment;
        public EquipmentViewModel LegsEquipment
        {
            get { return _legsEquipment; }
            set { _legsEquipment = value; RaisePropertyChanged(); }
        }

        public EquipmentViewModel _bootsEquipment;
        public EquipmentViewModel BootsEquipment
        {
            get { return _bootsEquipment; }
            set { _bootsEquipment = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<EquipmentViewModel> NinjaEquipments { get; set; }

        public ICommand ShowShopCommand { get; set; }

        private ShopWindow _shopWindow;


        public NinjaOverviewViewModel(ManageNinjasViewModel manageNinjas)
        {

            _manageNinjasModel = manageNinjas;

            SelectedNinja = _manageNinjasModel.SelectedNinja;

            NinjaEquipments = SelectedNinja.Equipments;
            NinjaEquipments.CollectionChanged += OnCollectionChanged;
           

            ShowShopCommand = new RelayCommand(ShowShop);
         
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetEquipmentTypes();

        }


        //check categorie and fill correct object 
        private void SetEquipmentTypes()
        {
            HeadEquipment = null;
            ShoulderEquipment = null;
            ChestEquipment = null;
            BeltEquipment = null;
            LegsEquipment = null;
            BootsEquipment = null;

            foreach (var item in SelectedNinja.Equipments)
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

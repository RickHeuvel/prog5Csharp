﻿using GalaSoft.MvvmLight;
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

      //  public EquipmentViewModel HeadEquipment { get; set; }

        public EquipmentViewModel ShoulderEquipment { get; set; }

        public EquipmentViewModel ChestEquipment { get; set; }

        public EquipmentViewModel BeltEquipment { get; set; }

        public EquipmentViewModel LegsEquipment { get; set; }

        public EquipmentViewModel BootsEquipment { get; set; }

        public ObservableCollection<EquipmentViewModel> NinjaEquipments { get; set; }

        public ICommand ShowShopCommand { get; set; }

        private ShopWindow _shopWindow;


        public NinjaOverviewViewModel(ManageNinjasViewModel manageNinjas)
        {

            _manageNinjasModel = manageNinjas;

            SelectedNinja = _manageNinjasModel.SelectedNinja;

            NinjaEquipments = SelectedNinja.Equipments;
          //  NinjaEquipments.CollectionChanged += OnCollectionChanged;
           

            ShowShopCommand = new RelayCommand(ShowShop);
         
        }

        //void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    SetEquipmentTypes();

        //}


        //check categorie and fill correct object 
        private void SetEquipmentTypes()
        {
            
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

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.ViewModel.EquipmentVMs;
using NinjaManager.ViewModel.NinjaVMs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class ShopViewModel : ViewModelBase
    {
        private ManageNinjasViewModel _manageNinjas;
        private ManageEquipmentViewModel _manageEquipment;

        public NinjaViewModel SelectedNinja { get; set; }

        public ObservableCollection<EquipmentViewModel> Equipment { get; set; }

        private string _selectedCategory;

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                ShowSelectedCategory(value);
            }
        }

        public ObservableCollection<EquipmentViewModel> SelectedEquipmentList { get; set; }


        private EquipmentViewModel _selectedEquipment;

        public EquipmentViewModel SelectedEquipment
        {
            get { return _selectedEquipment; }
            set { _selectedEquipment = value; RaisePropertyChanged(); RaisePropertyChanged("SellItemCommand"); RaisePropertyChanged("BuyItemCommand"); }
        }



        //Commands
        public ICommand BtnSelectCommand { get; set; }
        public RelayCommand BuyItemCommand { get { return new RelayCommand(BuyItem, CanBuyItem); } }
        public RelayCommand SellItemCommand { get { return new RelayCommand(SellItem, CanSellItem); } }
        public ICommand SellAllCommand { get; set; }

        public ShopViewModel(ManageNinjasViewModel manageNinjas, ManageEquipmentViewModel manageEquipment)
        {
            _manageNinjas = manageNinjas;
            _manageEquipment = manageEquipment;

            SelectedNinja = _manageNinjas.SelectedNinja;
            SelectedEquipmentList = new ObservableCollection<EquipmentViewModel>();
            Equipment = _manageEquipment.Equipment;

            BtnSelectCommand = new RelayCommand<string>(BtnSelectClick);
            SellAllCommand = new RelayCommand(SellAll);

            SelectedCategory = "Head";
            SelectedEquipment = SelectedEquipmentList.First();

        }

        public bool CanBuyItem()
        {

            if (SelectedEquipment != null)
            {
                var matchingEquipment = SelectedNinja.Equipments.ToList().Find(e => e.CategoryId == SelectedEquipment.CategoryId);
                if (SelectedNinja.Gold >= SelectedEquipment.Price && matchingEquipment == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        private void BuyItem()
        {
            using (var context = new NinjaDBEntities())
            {
                var ninja = context.Ninjas.Single(n => n.Id == SelectedNinja.Id);
                var equipment = context.Equipments.ToList().Find(e => e.Id == SelectedEquipment.Id);

                if (ninja != null)
                {
                    ninja.Equipments.Add(equipment);
                    ninja.Strenght += equipment.Strength;
                    ninja.Intelligence += equipment.Intelligence;
                    ninja.Agility += equipment.Agility;
                    ninja.Gold -= equipment.Price;
                    context.SaveChanges();

                    SelectedNinja.Equipments.Add(SelectedEquipment);
                    SelectedNinja.Strenght += SelectedEquipment.Strength;
                    SelectedNinja.Intelligence += SelectedEquipment.Intelligence;
                    SelectedNinja.Agility += SelectedEquipment.Agility;
                    SelectedNinja.Gold -= SelectedEquipment.Price;
                    int value = 0;
                    ninja.Equipments.ToList().ForEach(e => value += e.Price);
                    SelectedNinja.GearValue = value;
                }
            }
            RaisePropertyChanged("SellItemCommand"); 
            RaisePropertyChanged("BuyItemCommand");
        }

        private void SellItem()
        {
            using (var context = new NinjaDBEntities())
            {
                var ninja = context.Ninjas.Single(n => n.Id == SelectedNinja.Id);
                var equipment = ninja.Equipments.ToList().Find(e => e.Id == SelectedEquipment.Id);

                if (ninja != null && equipment != null)
                {
                    ninja.Equipments.Remove(equipment);
                    ninja.Strenght -= equipment.Strength;
                    ninja.Intelligence -= equipment.Intelligence;
                    ninja.Agility -= equipment.Agility;
                    ninja.Gold += equipment.Price;
                    context.SaveChanges();

                    var tempEquipment = SelectedNinja.Equipments.First(e => e.Id == SelectedEquipment.Id);
                    SelectedNinja.Equipments.Remove(tempEquipment);
                    SelectedNinja.Strenght -= SelectedEquipment.Strength;
                    SelectedNinja.Intelligence -= SelectedEquipment.Intelligence;
                    SelectedNinja.Agility -= SelectedEquipment.Agility;
                    SelectedNinja.Gold += SelectedEquipment.Price;
                    SelectedNinja.GearValue = SelectedNinja.GearValue - equipment.Price;
                }
            }
            RaisePropertyChanged("SellItemCommand");
            RaisePropertyChanged("BuyItemCommand");
        }
        private bool CanSellItem()
        {
            if (SelectedEquipment != null)
            {

                EquipmentViewModel temp = SelectedNinja.Equipments.ToList().Find(e => e.Id == SelectedEquipment.Id);

                if (temp != null)
                {
                    return true;
                }
            }
            return false;

        }
        private void SellAll()
        {
            using (var context = new NinjaDBEntities())
            {
                var ninja = context.Ninjas.Single(n => n.Id == SelectedNinja.Id);

                if (ninja != null)
                {
                    ninja.Gold += SelectedNinja.GearValue;
                    ninja.Intelligence = 0;
                    ninja.Strenght = 0;
                    ninja.Agility = 0;
                    ninja.Equipments.Clear();
                    context.SaveChanges();

                    SelectedNinja.Gold = ninja.Gold;
                    SelectedNinja.GearValue = 0;
                    SelectedNinja.Intelligence = 0;
                    SelectedNinja.Strenght = 0;
                    SelectedNinja.Agility = 0;
                    SelectedNinja.Equipments.ToList().ForEach(e => SelectedNinja.Equipments.Remove(e));
                }
            }
        }

        private void BtnSelectClick(string cat)
        {
            SelectedCategory = cat;
        }

        private void ShowSelectedCategory(string cat)
        {
            SelectedEquipmentList.Clear();
            Equipment.ToList().FindAll(e => e.Category.Name == cat).ForEach(e => SelectedEquipmentList.Add(e));
        }


    }
}

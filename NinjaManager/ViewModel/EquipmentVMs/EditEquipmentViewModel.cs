/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:NinjaManager"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NinjaManager.ViewModel.EquipmentVMs;
using NinjaManager.ViewModel.NinjaVMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace NinjaManager.ViewModel
{
    public class EditEquipmentViewModel : ViewModelBase
    {
        private ManageEquipmentViewModel _manageEquipment;

        public List<EquipmentCategoryViewModel> Categories { get; set; }
        public EquipmentViewModel SelectedEquipment { get; set; }

        public EquipmentCategoryViewModel SelectedCategory { get; set; }

        public string Name 
        {
            get { return SelectedEquipment.Name; }
            set { SelectedEquipment.Name = value; RaisePropertyChanged("EditEquipmentCommand"); } 
        }

        private string _strenght;
        public string Strength 
        { 
            get { return _strenght; }
            set { _strenght = value; RaisePropertyChanged("EditEquipmentCommand"); }
        }

        private string _intelligence;
        public string Intelligence 
        { get { return _intelligence; }
            set { _intelligence = value; RaisePropertyChanged("EditEquipmentCommand"); }
        }

        private string _agility;
        public string Agility
        { 
            get { return _agility; } 
            set { _agility = value; RaisePropertyChanged("EditEquipmentCommand"); }
        }

        private string _price;
        public string Price
        { 
            get { return _price; } 
            set { _price = value; RaisePropertyChanged("EditEquipmentCommand"); } 
        }

        public RelayCommand EditEquipmentCommand { get { return new RelayCommand(EditEquipment, CanEditEquipment); } }

        public EditEquipmentViewModel(ManageEquipmentViewModel manageEquipment)
        {
            _manageEquipment = manageEquipment;
            SelectedEquipment = _manageEquipment.SelectedEquipment;

            GetCategories();

            Strength = SelectedEquipment.Strength.ToString();
            Intelligence = SelectedEquipment.Intelligence.ToString();
            Agility = SelectedEquipment.Agility.ToString();
            Price = SelectedEquipment.Price.ToString();
          
        }

        private void GetCategories()
        {
            Categories = new List<EquipmentCategoryViewModel>();
            using (var context = new NinjaDBEntities())
            {
                context.EquipmentCategories.ToList().ForEach(c => Categories.Add(c.ToPoCo()));

                SelectedCategory = new EquipmentCategoryViewModel(context.Equipments
                    .Single(e => e.Id == SelectedEquipment.Id).EquipmentCategory);
            }

        }
        private void EditEquipment()
        {

            SelectedEquipment.Category = SelectedCategory;

            using (var context = new NinjaDBEntities())
            {
                var equipment = context.Equipments.Single(e => e.Id == SelectedEquipment.Id);

                if (equipment != null)
                {
                    equipment.Name = SelectedEquipment.Name;
                    equipment.Strength = SelectedEquipment.Strength;
                    equipment.Intelligence = SelectedEquipment.Intelligence;
                    equipment.Agility = SelectedEquipment.Agility;
                    equipment.CategoryId = SelectedCategory.CategoryId;
                    equipment.Price = SelectedEquipment.Price;

                    context.SaveChanges();
                }
            }
            _manageEquipment.CloseEditEquipment();
        }

        private bool CanEditEquipment()
        {
            if (Name == null || Name.Length < 1 || Name.StartsWith(" "))
            {
                return false;
            }
            else if (!int.TryParse(Strength, out _))
            {
                return false;
            }
            else if (!int.TryParse(Intelligence, out _))
            {
                return false;
            }
            else if (!int.TryParse(Agility, out _))
            {
                return false;
            }
            else if (!int.TryParse(Price, out int result))
            {
                return false;
            }
            else if (result < 1)
            {
                return false;
            }
            SelectedEquipment.Name = Name;
            SelectedEquipment.Strength = int.Parse(Strength);
            SelectedEquipment.Intelligence = int.Parse(Intelligence);
            SelectedEquipment.Agility = int.Parse(Agility);
            SelectedEquipment.Price = int.Parse(Price);
            return true;
        }
    }
}